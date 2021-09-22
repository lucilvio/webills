using Autofac;
using Lucilvio.Solo.Architecture.Outbox.Infrastructure;

namespace Lucilvio.Solo.Architecture.Outbox
{
    public static class Initializer
    {
        public static ContainerBuilder RegisterOutbox(this ContainerBuilder builder)
        {
            builder.RegisterType<OutboxDataAccess>().As<IOutboxDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<Outbox, IEventPublisher>();

            return builder;
        }
    }
}