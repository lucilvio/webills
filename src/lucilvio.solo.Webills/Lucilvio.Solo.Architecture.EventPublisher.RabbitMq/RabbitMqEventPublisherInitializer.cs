using Autofac;
using Lucilvio.Solo.Architecture.EventPublisher.Outbox;

namespace Lucilvio.Solo.Architecture.EventPublisher.RabbitMq
{
    public static class RabbitMqEventPublisherInitializer
    {
        public static ContainerBuilder AddRabbitMqEventPublisher(this ContainerBuilder builder,
            EventPublisherConfigurations configurations)
        {
            builder.Register<IEventPublisher>(context => new RabbitMqEventPublisher(configurations));

            return builder;
        }

        public static ContainerBuilder AddRabbitMqEventPublisherWithOutbox(this ContainerBuilder builder,
            EventPublisherConfigurations configurations)
        {
            builder.Register<IEventPublisher>(context => new RabbitMqEventPublisher(configurations));
            builder.RegisterOutbox();

            return builder;
        }
    }
}
