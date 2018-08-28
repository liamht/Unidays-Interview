using System.Data;
using Unidays.Interview.Common;
using Unidays.Interview.Domain.Entities;

namespace Unidays.Interview.Data.Users.Commands
{
    public class CreateUserDbCommand : ICreateUserDbCommand
    {
        private readonly IDbCommand _command;

        public CreateUserDbCommand(IDbCommand command)
        {
            _command = command;
            _command.CommandText = "EXEC CreateUser @email = @userEmail, @password = @userPassword, @salt = @userSalt";
        }

        public void Execute(User user)
        {
            Guard.NotNull(user?.EmailAddress, "Email Address");
            Guard.NotNull(user?.Password, "Password");
            Guard.NotNull(user?.Salt, "Salt");

            AddParameter("@email", user.EmailAddress);
            AddParameter("@userPassword", user.Password);
            AddParameter("@userSalt", user.Salt);

            _command.Connection.Open();
            _command.ExecuteNonQuery();
            _command.Connection.Close();

            _command.Dispose();
        }

        private void AddParameter(string parameterName, string value)
        {
            var parameter = _command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            _command.Parameters.Add(parameter);
        }
    }
}
