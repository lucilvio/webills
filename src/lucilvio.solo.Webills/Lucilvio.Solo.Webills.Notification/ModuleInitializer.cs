using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Modules.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Notifications
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddNotificationsModule(this IServiceCollection services,
            Configurations configurations)
        {
            services.AddSingleton<IModuleResolver<INotificationModule>>(provider
                  => new AutofacModuleResolver<INotificationModule>(configurations));

            services.AddSingleton<INotificationModule>(provider
                => new NotificationsModule(provider.GetService<IModuleResolver<INotificationModule>>()));

            return services;
        }
    }
}