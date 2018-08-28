using Microsoft.AspNetCore.Mvc;
using Unidays.Interview.ApplicationServices.Users.Commands;
using Unidays.Interview.ApplicationServices.Users.Queries;
using Unidays.Interview.UI.ViewModels;

namespace Unidays.Interview.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly ICreateUserCommand _createUserCommand;
        private readonly IUserExistsQuery _userExistsQuery;

        public UserController(ICreateUserCommand createUserCommand, IUserExistsQuery userExistsQuery)
        {
            _createUserCommand = createUserCommand;
            _userExistsQuery = userExistsQuery;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserCreationViewModel());
        }

        [HttpPost]
        public IActionResult Create(UserCreationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var doesUserAlreadyExist = _userExistsQuery.Execute(vm.EmailAddress);

            if (doesUserAlreadyExist)
            {
                ModelState.AddModelError(nameof(vm.EmailAddress), "Email address is already in use");
                return View(vm);
            }

            _createUserCommand.Execute(vm.EmailAddress, vm.UnencryptedPassword);

            return RedirectToAction("Index", "Home", new { message = $"User '{vm.EmailAddress}' successfully created!" });
        }
    }
}