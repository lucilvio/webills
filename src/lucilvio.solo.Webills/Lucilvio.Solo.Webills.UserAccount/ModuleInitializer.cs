using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox;
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
            var moduleResolver = new AutofacModuleResolver<IUserAccountModule>(c =>
            {
                c.Register(_ => new UserAccountDataContext(configurations)).InstancePerLifetimeScope();

                c.RegisterOutbox(configurations.DataConnectionString, configurations.ModuleName);
                c.AddRabbitMqEventPublisher(new EventPublisherConfigurations { Host = "localhost" });
            });

            var module = new UserAccountModule(moduleResolver);

            services.AddSingleton<IUserAccountModule>(module);
        }
    }
}