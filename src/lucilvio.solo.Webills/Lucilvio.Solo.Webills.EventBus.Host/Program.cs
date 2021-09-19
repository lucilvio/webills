using Lucilvio.Solo.Webills.Notification;
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
                    var eventPublisher = services.AddRabbitMqEventPublisher(new Configurations
                    {
                        Host = "localhost"
                    });

                    services.AddNotificationsModule(new Module.Configurations
                    {
                        DataConnectionString = "Server=localhost;Database=lucilvio.solo.webills;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=300;"
                    }, eventPublisher);

                    services.AddHostedService<Worker>();
                });
    }
}
