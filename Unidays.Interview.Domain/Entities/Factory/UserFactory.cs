using Unidays.Interview.Common;

namespace Unidays.Interview.Domain.Entities.Factory
{
    public class UserFactory : IUserFactory
    {
        public User Create(string email, string password)
        {
            Guard.NotNull(email, "Email Address");
            Guard.NotNull(password, "Password");

            var salt = EncryptionHelpers.CreateSalt(10);
            var encryptedPassword = EncryptionHelpers.GetHashedString(password, salt);

            return new User()
            {
                EmailAddress = email,
                Password = encryptedPassword,
                Salt = salt
            };
        }

        public User Create(string email)
        {
            Guard.NotNull(email, "Email Address");

            return new User()
            {
                EmailAddress = email
            };
        }
    }
}
