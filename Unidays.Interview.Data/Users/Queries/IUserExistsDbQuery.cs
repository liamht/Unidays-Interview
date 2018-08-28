using Unidays.Interview.Domain.Entities;

namespace Unidays.Interview.Data.Users.Queries
{
    public interface IUserExistsDbQuery
    {
        bool Execute(User user);
    }
}