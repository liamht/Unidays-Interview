using Moq;
using Unidays.Interview.ApplicationServices.Users.Queries;
using Unidays.Interview.Data.Users.Queries;
using Unidays.Interview.Domain.Entities;
using Unidays.Interview.Domain.Entities.Factory;
using Xunit;

namespace Unidays.Interview.ApplicationServices.Test.Users.Queries
{
    public class UserExistsQueryTests
    {
        private Mock<IUserExistsDbQuery> _dbCommand;
        private Mock<IUserFactory> _factory;
        private UserExistsQuery _subject;
        private User _user;

        public UserExistsQueryTests()
        {
            _user = new User() { EmailAddress = "email@test.com" };

            _factory = new Mock<IUserFactory>();
            _factory.Setup(c => c.Create(It.IsAny<string>())).Returns(_user);

            _dbCommand = new Mock<IUserExistsDbQuery>();
            _dbCommand.Setup(c => c.Execute(It.IsAny<User>())).Returns(false);

            _subject = new UserExistsQuery(_factory.Object, _dbCommand.Object);
        }

        [Fact]
        public void Execute_CreatesUserWithFactory()
        {
            var email = "email";
            _subject.Execute(email);

            _factory.Verify(c => c.Create(It.Is<string>(x => x == email)), Times.Once());
        }

        [Fact]
        public void Execute_CallsDbQueryWithObjectReturnedFromFactory()
        {
            var email = "email";
            _subject.Execute(email);

            _dbCommand.Verify(c => c.Execute(It.Is<User>(x => x == _user)), Times.Once);
        }

        [Fact]
        public void Execute_WhenUserExists_ReturnsTrue()
        {
            _dbCommand.Setup(c => c.Execute(It.IsAny<User>())).Returns(true);

            var result = _subject.Execute("any email");

            Assert.True(result);
        }

        [Fact]
        public void Execute_WhenUserDoesNotExists_ReturnsFalse()
        {
            _dbCommand.Setup(c => c.Execute(It.IsAny<User>())).Returns(false);

            var result = _subject.Execute("any email");

            Assert.False(result);
        }
    }
}
