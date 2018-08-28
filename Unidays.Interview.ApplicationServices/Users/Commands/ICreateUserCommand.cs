namespace Unidays.Interview.ApplicationServices.Users.Commands
{
    public interface ICreateUserCommand
    {
        void Execute(string username, string password);
    }
}