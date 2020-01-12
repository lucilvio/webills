using System.Linq;
using Lucilvio.Solo.Webills.Infraestructure.EFDataStorage;
using Lucilvio.Solo.Webills.Profile.Domain.User;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            using(var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<WebillsContext>())
                {
                    context.Database.Migrate();

                    var user = context.Users.FirstOrDefault(u => u.Login == new Login("admin@mail.com"));

                    if(user == null)
                    {
                        context.Users.Add(new User("Admin", new Login("admin@mail.com"), new Password("123456"), true));
                        context.SaveChanges();
                    }
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
