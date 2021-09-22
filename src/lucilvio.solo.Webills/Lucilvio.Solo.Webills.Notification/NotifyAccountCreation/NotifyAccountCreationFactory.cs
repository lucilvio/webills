using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Inbox;
using Lucilvio.Solo.Webills.Notification.Infrastructure.DataAccess;
using Lucilvio.Solo.Webills.Notification.Infrastructure.Injection;
using Lucilvio.Solo.Webills.Notification.NotifyAccountCreation;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Notification.Infrastructure.AutofacModule
{
    internal class NotifyAccountCreationFactory : AutofacFactory
    {
        public NotifyAccountCreationFactory(Module.Configurations configurations) : base(configurations) { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<DbContext>(ctx => new NotificationDataContext(base._configurations.DataConnectionString)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<NotifyAccountCreation.NotifyAccountCreation>().As<IHandler<AccountCreatedMessage>>()
                .InstancePerLifetimeScope();

            builder.RegisterInbox<AccountCreatedMessage>();
            builder.RegisterLogHandler<AccountCreatedMessage>();

            base.Load(builder);
        }
    }
}