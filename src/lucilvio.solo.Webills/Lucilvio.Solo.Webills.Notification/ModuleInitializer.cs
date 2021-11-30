using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Modules.AutofacModule;
using Lucilvio.Solo.Webills.Notifications.Infrastructure;
using Lucilvio.Solo.Webills.Notifications.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Notifications
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddNotificationsModule(this IServiceCollection services,
            Configurations configurations)
        {
            var moduleResolver = new AutofacModuleResolver<INotificationModule>(c =>
            {
                c.Register(_ => new NotificationDataContext(configurations.DataConnectionString))
                    .As<DbContext>().InstancePerLifetimeScope();

                c.RegisterType<NotificationService>().As<INotificationService>().SingleInstance();
            });

            var module = new NotificationsModule(moduleResolver);

            services.AddSingleton<INotificationModule>(module);
            services.AddSingleton<IEventListener>(module);

            return services;
        }
    }
}