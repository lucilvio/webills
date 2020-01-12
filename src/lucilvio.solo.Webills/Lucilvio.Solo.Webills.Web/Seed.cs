using System.Linq;
using Lucilvio.Solo.Webills.Infraestructure.EFDataStorage;
using Lucilvio.Solo.Webills.Profile.Domain.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public static class Seed
    {
        public static void RunSeeder(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<WebillsContext>())
                {
                    context.Database.Migrate();

                    var user = context.Users.FirstOrDefault(u => u.Login == new Login("admin@mail.com"));

                    if (user == null)
                    {
                        context.Users.Add(new User("Admin", new Login("admin@mail.com"), new Password("123456"), true));
                        context.SaveChanges();
                    }
                }
            }
        }
    }

}
