using System.Collections.Generic;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Notification.Infrastructure.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Notification
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddNotificationsModule(this IServiceCollection services, Module.Configurations configurations,
            Module.EventsToSubscribe eventsToSubscribe, IEventBus eventBus)
        {
            var module = new NotificationModule(configurations, eventsToSubscribe, eventBus);
            services.AddSingleton(module);

            return services;
        }
    }
}