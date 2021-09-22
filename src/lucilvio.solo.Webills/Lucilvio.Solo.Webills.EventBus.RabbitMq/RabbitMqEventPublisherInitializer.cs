using Autofac;
using Lucilvio.Solo.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.EventBus.RabbitMq
{
    public static class RabbitMqEventPublisherInitializer
    {
        public static IServiceCollection AddRabbitMqEventPublisher(this IServiceCollection services,
            Configurations configurations)
        {
            services.AddSingleton(provider => new RabbitMqEventPublisher(configurations));
            return services;
        }

        public static ContainerBuilder AddRabbitMqEventPublisher(this ContainerBuilder builder,
            Configurations configurations)
        {
            builder.Register<IEventPublisher>(context => new RabbitMqEventPublisher(configurations));

            return builder;
        }
    }
}
