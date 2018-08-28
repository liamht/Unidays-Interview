using Unidays.Interview.Domain.Entities;

namespace Unidays.Interview.Data.Users.Commands
{
    public interface ICreateUserDbCommand
    {
        void Execute(User user);
    }
}