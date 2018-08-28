using System.Data;
using Unidays.Interview.Common;
using Unidays.Interview.Domain.Entities;

namespace Unidays.Interview.Data.Users.Queries
{
    public class UserExistsDbQuery : IUserExistsDbQuery
    {
        private readonly IDbCommand _command;

        public UserExistsDbQuery(IDbCommand command)
        {
            _command = command;
            _command.CommandText = "EXEC DoesUserExist @emailaddress = @email";
        }

        public bool Execute(User user)
        {            
            Guard.NotNull(user?.EmailAddress, "Email Address");

            AddParameter("@email", user.EmailAddress);

            _command.Connection.Open();            

            bool response = (bool)_command.ExecuteScalar();

            _command.Dispose();
            _command.Connection.Close();

            return response;
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

