using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddUserAccountModule(this IServiceCollection services, Module.Configurations configurations,
            IEventBus eventBus)
        {
            services.AddSingleton<Module>(provider => new UserAccountModule(configurations, eventBus));

            return services;
        }

        public static ContainerBuilder AddModule(this ContainerBuilder builder, Module.Configurations configurations,
            IEventBus eventBus)
        {
            builder.Register<Module>(ctx => new UserAccountModule(configurations, eventBus)).SingleInstance();

            return builder;
        }
    }
}