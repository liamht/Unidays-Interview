using Unidays.Interview.Common;
using Unidays.Interview.Domain.Entities.Factory;
using Xunit;

namespace Unidays.Interview.Domain.Test.Entities.Factory
{
    public class UserFactoryTests
    {
        private UserFactory _subject;

        public UserFactoryTests()
        {
            _subject = new UserFactory();
        }

        [Fact]
        public void Create_WithUsernameAndPassword_CreatesUser()
        {
            var givenEmail = "email";
            var givenPassword = "password";
            var result = _subject.Create(givenEmail, givenPassword);

            Assert.NotNull(result);
            Assert.NotNull(result.EmailAddress);
            Assert.NotNull(result.Password);
        }

        [Fact]
        public void Create_WithUsernameAndPassword_CreatesRandomSalt()
        {
            var givenEmail = "email";
            var givenPassword = "password";
            var result = _subject.Create(givenEmail, givenPassword);

            Assert.NotNull(result.Salt);
            Assert.NotEqual(result.Salt, result.EmailAddress);
            Assert.NotEqual(result.Salt, result.Password);
        }

        [Fact]
        public void Create_WithUsernameAndPassword_EncryptsPassword()
        {
            var givenEmail = "email";
            var givenPassword = "password";
            var result = _subject.Create(givenEmail, givenPassword);

            var encryptedPassword = EncryptionHelpers.GetHashedString(givenPassword, result.Salt);

            Assert.Equal(encryptedPassword, result.Password);
        }

        [Fact]
        public void Create_WithUsernameOnly_CreatesUser()
        {
            var result = _subject.Create("test");

            Assert.NotNull(result);
            Assert.NotNull(result.EmailAddress);
        }

        [Fact]
        public void Create_WithUsernameOnly_DoesNotCreatePasswordOrSalt()
        {
            var result = _subject.Create("test");

            Assert.Null(result.Password);
            Assert.Null(result.Salt);
        }
    }
}
