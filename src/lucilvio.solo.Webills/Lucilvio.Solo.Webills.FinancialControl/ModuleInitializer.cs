using System;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Modules.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddFinancialControleModule(this IServiceCollection services,
            Configurations configurations)
        {
            var interceptors = new Type[]
            {
                typeof(AuthorizationInterceptor<>),
            };

            services.AddSingleton<IModuleResolver<IFinancialControlModule>>(provider =>
                new AutofacModuleResolver<IFinancialControlModule>(configurations, interceptors));

            services.AddSingleton<IFinancialControlModule>(provider
                => new FinancialControlModule(provider.GetService<IModuleResolver<IFinancialControlModule>>()));

            return services;
        }
    }
}
