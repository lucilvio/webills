using System;
using Autofac;
using Lucilvio.Solo.Architecture.Handler.Inbox.Component;
using Lucilvio.Solo.Architecture.Handler.Inbox.Component.Infrastructure;

namespace Lucilvio.Solo.Architecture.Handler.Inbox
{
    public static class Initializer
    {
        public static ContainerBuilder RegisterInbox(this ContainerBuilder builder, Type messageType)
        {
            builder.RegisterType<InboxDataAccess>().As<IInboxDataAccess>().InstancePerLifetimeScope();

            var inboxInterfaceType = typeof(IInbox<>).MakeGenericType(messageType);
            var inboxType = typeof(Inbox<>).MakeGenericType(messageType);

            builder.RegisterType(inboxType).As(inboxInterfaceType).InstancePerLifetimeScope();

            return builder;
        }
    }
}