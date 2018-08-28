namespace Unidays.Interview.Domain.Entities.Factory
{
    public interface IUserFactory
    {
        User Create(string email, string password);
        User Create(string email);
    }
}