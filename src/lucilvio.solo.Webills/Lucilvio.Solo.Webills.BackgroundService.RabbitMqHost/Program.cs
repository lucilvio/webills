using Lucilvio.Solo.Webills.Notifications;
using Lucilvio.Solo.Webills.UserAccount;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lucilvio.Solo.Webills.EventBus.RabbitMq.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddNotificationsModule(new Notifications.Configurations
                    {
                        DataConnectionString = "Server=localhost;Database=lucilvio.solo.webills;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=300;"
                    });

                    services.AddUserAccountModule(new UserAccount.Configurations
                    {
                        DataConnectionString = "Server=localhost;Database=lucilvio.solo.webills;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=300;",
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
