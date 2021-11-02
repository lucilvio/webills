using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Design;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<UserAccountDataContext>
    {
        public UserAccountDataContext CreateDbContext(string[] args)
        {
            Debugger.Launch();
            var configurations = new Configurations
            {
                DefaultAccount = new Configurations.DefaultUserAccount
                {
                    Name = "Admin",
                    Password = "123456",
                    Email = "admin@mail.com",
                },
                DataConnectionString = args[0]
            };

            return new UserAccountDataContext(configurations);
        }
    }
}