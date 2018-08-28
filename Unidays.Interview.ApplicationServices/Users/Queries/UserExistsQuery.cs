using Unidays.Interview.Data.Users.Queries;
using Unidays.Interview.Domain.Entities.Factory;

namespace Unidays.Interview.ApplicationServices.Users.Queries
{
    public class UserExistsQuery : IUserExistsQuery
    {
        private readonly IUserFactory _factory;
        private readonly IUserExistsDbQuery _dbQuery;

        public UserExistsQuery(IUserFactory factory, IUserExistsDbQuery dbQuery)
        {
            _factory = factory;
            _dbQuery = dbQuery;
        }

        public bool Execute(string emailAddress)
        {
            var user = _factory.Create(emailAddress);

            return _dbQuery.Execute(user);
        }
    }
}
