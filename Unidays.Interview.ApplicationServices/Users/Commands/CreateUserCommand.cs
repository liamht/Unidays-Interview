using Unidays.Interview.Data.Users.Commands;
using Unidays.Interview.Domain.Entities.Factory;

namespace Unidays.Interview.ApplicationServices.Users.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IUserFactory _factory;
        private readonly ICreateUserDbCommand _dbCommand;

        public CreateUserCommand(IUserFactory factory, ICreateUserDbCommand dbCommand)
        {
            _factory = factory;
            _dbCommand = dbCommand;
        }

        public void Execute(string username, string password)
        {
            var user = _factory.Create(username, password);
            _dbCommand.Execute(user);
        }
    }
}
