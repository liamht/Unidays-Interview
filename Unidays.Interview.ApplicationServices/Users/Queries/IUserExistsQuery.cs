namespace Unidays.Interview.ApplicationServices.Users.Queries
{
    public interface IUserExistsQuery
    {
        bool Execute(string emailAddress);
    }
}