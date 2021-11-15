using Autofac;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component.Infrastructure;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox
{
    public static class Initializer
    {
        public static ContainerBuilder RegisterOutbox(this ContainerBuilder builder)
        {
            builder.RegisterType<OutboxDataAccess>().As<IOutboxDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<Component.Outbox, IEventPublisher>();

            return builder;
        }
    }
}