using Autofac;
using Lucilvio.Solo.Architecture.Inbox;

namespace Lucilvio.Solo.Architecture.Handler.Inbox
{
    public static class Initializer
    {
        public static ContainerBuilder RegisterInbox<TMessage>(this ContainerBuilder builder)
            where TMessage : Message
        {
            builder.RegisterType<InboxDataAccess>().As<IInboxDataAccess>().InstancePerLifetimeScope();
            builder.RegisterType<Inbox<TMessage>>().AsSelf().InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder RegisterLogHandler(this ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(LogHandler<>), typeof(IHandler<>));
            return builder;
        }

        public static ContainerBuilder RegisterLogHandler<TMessage>(this ContainerBuilder builder)
            where TMessage : Message
        {
            builder.RegisterDecorator<LogHandler<TMessage>, IHandler<TMessage>>();
            return builder;
        }
    }
}