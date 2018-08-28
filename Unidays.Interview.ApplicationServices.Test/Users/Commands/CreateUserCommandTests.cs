using Moq;
using Unidays.Interview.ApplicationServices.Users.Commands;
using Unidays.Interview.Data.Users.Commands;
using Unidays.Interview.Domain.Entities;
using Unidays.Interview.Domain.Entities.Factory;
using Xunit;

namespace Unidays.Interview.ApplicationServices.Test.Users.Commands
{
    public class CreateUserCommandTests
    {
        private Mock<ICreateUserDbCommand> _dbCommand;
        private Mock<IUserFactory> _factory;
        private CreateUserCommand _subject;
        private User _user;

        public CreateUserCommandTests()
        {
            _user = new User() { EmailAddress = "email@test.com", Password = "password", Salt = "salt" };

            _factory = new Mock<IUserFactory>();
            _factory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(_user);

            _dbCommand = new Mock<ICreateUserDbCommand>();
            _dbCommand.Setup(c => c.Execute(It.IsAny<User>()));

            _subject = new CreateUserCommand(_factory.Object, _dbCommand.Object);
        }

        [Fact]
        public void Execute_CreatesUserWithFactory()
        {
            var email = "email";
            var password = "password";
            _subject.Execute(email, password);

            _factory.Verify(c => c.Create(It.Is<string>(x => x == email), It.Is<string>(x => x == password)), Times.Once());
        }

        [Fact]
        public void Execute_CallsDbCommandWithObjectReturnedFromFactory()
        {
            var email = "email";
            var password = "password";
            _subject.Execute(email, password);

            _dbCommand.Verify(c => c.Execute(It.Is<User>(x => x == _user)), Times.Once);
        }
    }
}
