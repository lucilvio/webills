using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Handler.Inbox;
using Lucilvio.Solo.Webills.Notifications.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Notifications.NotifyAccountCreation
{
    internal class NotifyAccountCreationFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            var configurations = parameters as Configurations;

            container.Register<DbContext>(ctx => new NotificationDataContext(configurations.DataConnectionString))
                .AsSelf().InstancePerLifetimeScope();

            container.RegisterType<NotifyAccountCreation>().As<IHandler<AccountCreatedMessage>>()
                .InstancePerLifetimeScope();

            container.RegisterInbox<AccountCreatedMessage>();
            container.RegisterLogHandler<AccountCreatedMessage>();
        }
    }
}