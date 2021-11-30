using Autofac;

namespace Lucilvio.Solo.Architecture.EventPublisher.RabbitMq
{
    public static class RabbitMqEventPublisherInitializer
    {
        public static ContainerBuilder AddRabbitMqEventPublisher(this ContainerBuilder builder,
            EventPublisherConfigurations configurations)
        {
            builder.Register<IEventPublisher>(_ => new RabbitMqEventPublisher(configurations))
                .SingleInstance();

            return builder;
        }
    }
}
