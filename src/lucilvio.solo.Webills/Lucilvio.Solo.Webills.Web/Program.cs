using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

                    var user = context.Users.FirstOrDefault();

                    if(user == null)
                    {
                        context.Users.Add(new User("Test User"));
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
