using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Notification.Infrastructure.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Notification
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddNotificationsModule(this IServiceCollection services, Module.Configurations configurations,
            IEventPublisher eventBus)
        {
            services.AddSingleton<Module>(provider => new NotificationModule(configurations, eventBus));

            return services;
        }
    }
}