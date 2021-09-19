using Lucilvio.Solo.Webills.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddUserAccountModule(this IServiceCollection services, Module.Configurations configurations,
            IEventPublisher eventBus)
        {
            services.AddSingleton<Module>(provider => new UserAccountModule(configurations, eventBus));

            return services;
        }

        public static IServiceCollection AddUserAccountModule(this IServiceCollection services,
            IEventPublisher eventBus, Module.Configurations configurations)
        {
            services.AddSingleton<Module>(provider => new UserAccountModule(configurations, eventBus));

            return services;
        }
    }
}