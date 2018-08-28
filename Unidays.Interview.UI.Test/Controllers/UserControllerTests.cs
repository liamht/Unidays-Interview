using Microsoft.AspNetCore.Mvc;
using Moq;
using Unidays.Interview.ApplicationServices.Users.Commands;
using Unidays.Interview.ApplicationServices.Users.Queries;
using Unidays.Interview.UI.Controllers;
using Unidays.Interview.UI.ViewModels;
using Xunit;

namespace Unidays.Interview.UI.Test.Controllers
{
    public class UserControllerTests
    {
        private UserController _subject;
        private Mock<ICreateUserCommand> _createUserCommand;
        private Mock<IUserExistsQuery> _userExistsQuery;

        public UserControllerTests()
        {
            _createUserCommand = new Mock<ICreateUserCommand>();
            _createUserCommand.Setup(c => c.Execute(It.IsAny<string>(), It.IsAny<string>()));

            _userExistsQuery = new Mock<IUserExistsQuery>();
            _userExistsQuery.Setup(c => c.Execute(It.IsAny<string>())).Returns(false);

            _subject = new UserController(_createUserCommand.Object, _userExistsQuery.Object);
        }

        [Fact]
        public void Create_OnGet_ReturnsDefaultView()
        {
            var result = _subject.Create() as ViewResult;

            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Create_OnGet_PassesEmptyViewModelToView()
        {
            var result = _subject.Create() as ViewResult;

            var emptyViewModel = new UserCreationViewModel();
            var returnedViewModel = result.Model as UserCreationViewModel;

            Assert.Equal(emptyViewModel.EmailAddress, returnedViewModel.EmailAddress);
            Assert.Equal(emptyViewModel.UnencryptedPassword, returnedViewModel.UnencryptedPassword);
        }

        [Fact]
        public void Create_OnPost_RedirectsToHomePageWithMessage()
        {
            var vm =  new UserCreationViewModel() { UnencryptedPassword = "test password", EmailAddress = "email@email.com" };

            var result = _subject.Create(vm) as RedirectToActionResult;

            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("Index", result.ActionName);
            Assert.NotNull(result.RouteValues["message"]);
        }

        [Fact]
        public void Create_OnPost_CreatesUser()
        {
            var vm = new UserCreationViewModel() { UnencryptedPassword = "test password", EmailAddress = "email@email.com" };
            _subject.Create(vm);

            _createUserCommand.Verify(c => c.Execute(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Create_OnPost_ChecksUserExists()
        {
            var vm = new UserCreationViewModel() { UnencryptedPassword = "test password", EmailAddress = "email@email.com" };
            _subject.Create(vm);

            _userExistsQuery.Verify(c => c.Execute(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Create_OnPost_WhenModelStateHasError_ReturnsToView()
        {
            _subject.ModelState.AddModelError("error", "this is an error");

            var vm = new UserCreationViewModel() { UnencryptedPassword = "test password", EmailAddress = "email@email.com" };
            var result = _subject.Create(vm) as ViewResult;

            Assert.Null(result.ViewName);
            Assert.Equal(vm, result.Model as UserCreationViewModel);
        }

        [Fact]
        public void Create_OnPost_WhenUserAlreadyExists_ReturnsToViewWithModalError()
        {
            _userExistsQuery.Setup(c => c.Execute(It.IsAny<string>())).Returns(true);
            
            var vm = new UserCreationViewModel() { UnencryptedPassword = "test password", EmailAddress = "email@email.com" };
            var result = _subject.Create(vm) as ViewResult;

            var errorMessage = _subject.ModelState["EmailAddress"].Errors[0].ErrorMessage;

            Assert.Single(_subject.ModelState);
            Assert.Equal("Email address is already in use", errorMessage);
        }
    }
}
