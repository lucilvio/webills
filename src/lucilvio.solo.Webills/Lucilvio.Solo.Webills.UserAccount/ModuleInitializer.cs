using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.EventPublisher.RabbitMq;
using Lucilvio.Solo.Architecture.Modules.AutofacModule;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public static class ModuleInitializer
    {
        public static void AddUserAccountModule(this IServiceCollection services,
            Configurations configurations)
        {
            services.AddSingleton<IModuleResolver<IUserAccountModule>>(_ =>
                new AutofacModuleResolver<IUserAccountModule>(c =>
                {
                    c.Register<DbContext>(_ => new UserAccountDataContext(configurations))
                        .InstancePerLifetimeScope();

                    c.Register(_ => new UserAccountDataContext(configurations))
                        .InstancePerLifetimeScope();

                    c.AddRabbitMqEventPublisher(new EventPublisherConfigurations { Host = "localhost" });
                })
            );

            services.AddSingleton<IUserAccountModule>(provider =>
                new UserAccountModule(provider.GetService<IModuleResolver<IUserAccountModule>>()));
        }
    }
}