using Autofac;
using Lucilvio.Solo.Architecture.Handler.Inbox.Component;
using Lucilvio.Solo.Architecture.Handler.Inbox.Component.Infrastructure;

namespace Lucilvio.Solo.Architecture.Handler.Inbox
{
    public static class Initializer
    {
        public static ContainerBuilder RegisterInbox<TMessage>(this ContainerBuilder builder)
            where TMessage : Message
        {
            builder.RegisterType<InboxDataAccess>().As<IInboxDataAccess>().InstancePerLifetimeScope();
            builder.RegisterType<Inbox<TMessage>>().As<IInbox<TMessage>>().InstancePerLifetimeScope();

            return builder;
        }
    }
}