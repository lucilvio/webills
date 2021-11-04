using Autofac;
using Lucilvio.Solo.Architecture.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Architecture.EventPublisher.RabbitMq
{
    public static class RabbitMqEventPublisherInitializer
    {
        public static IServiceCollection AddRabbitMqEventPublisher(this IServiceCollection services,
            EventPublisherConfigurations configurations)
        {
            services.AddSingleton(provider => new RabbitMqEventPublisher(configurations));
            return services;
        }

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
