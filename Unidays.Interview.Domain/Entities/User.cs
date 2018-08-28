using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unidays.Interview.Data.Test")]
[assembly: InternalsVisibleTo("Unidays.Interview.ApplicationServices.Test")]
namespace Unidays.Interview.Domain.Entities
{
    public class User
    {
        public string EmailAddress { get; internal set; }

        public string Password { get; internal set; }

        public string Salt { get; internal set; }
    }
}
