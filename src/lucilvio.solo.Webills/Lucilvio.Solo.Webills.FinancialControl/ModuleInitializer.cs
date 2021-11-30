using System.Data;
using System.Data.SqlClient;
using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Modules.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddFinancialControlModule(this IServiceCollection services,
            Configurations configurations)
        {
            services.AddSingleton<IModuleResolver<IFinancialControlModule>>(_ =>
                new AutofacModuleResolver<IFinancialControlModule>(c =>
                {
                    c.Register(_ => new SqlConnection(configurations.DataConnectionString)).As<IDbConnection>().InstancePerLifetimeScope();
                }));

            services.AddSingleton<IFinancialControlModule>(provider
                => new FinancialControlModule(provider.GetService<IModuleResolver<IFinancialControlModule>>()));

            return services;
        }
    }
}
