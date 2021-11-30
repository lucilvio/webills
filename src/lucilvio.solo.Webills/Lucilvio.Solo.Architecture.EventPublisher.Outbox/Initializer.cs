using Autofac;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component.Infrastructure;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox
{
    public static class Initializer
    {
        public static ContainerBuilder RegisterOutbox(this ContainerBuilder builder,
            string connectionString, string schema, string table = "OutgoingEvents")
        {
            builder.Register(_ => new OutboxDataContext(connectionString, schema, table))
                .AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<OutboxDataAccess>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterDecorator<Component.Outbox, IEventPublisher>();

            return builder;
        }
    }
}