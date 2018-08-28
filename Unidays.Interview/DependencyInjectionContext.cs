using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using Unidays.Interview.ApplicationServices.Users.Commands;
using Unidays.Interview.ApplicationServices.Users.Queries;
using Unidays.Interview.Data.Users.Commands;
using Unidays.Interview.Data.Users.Queries;
using Unidays.Interview.Domain.Entities.Factory;

namespace Unidays.Interview.UI
{
    public static class DependencyInjectionContext
    {
        public static void Bind(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbCommand>(c => new SqlCommand(null, new SqlConnection(connectionString)));

            services.AddScoped<IUserFactory, UserFactory>();

            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<IUserExistsQuery, UserExistsQuery>();

            services.AddScoped<IUserExistsDbQuery, UserExistsDbQuery>();
            services.AddScoped<ICreateUserDbCommand, CreateUserDbCommand>();
        }
    }
}
