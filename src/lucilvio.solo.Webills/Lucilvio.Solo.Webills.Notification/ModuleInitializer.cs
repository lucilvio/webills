using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Notification
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddNotificationsModule(this IServiceCollection services,
            Module.Configurations configurations)
        {
            services.AddSingleton<Module>(provider => new NotificationModule(configurations));

            return services;
        }
    }
}