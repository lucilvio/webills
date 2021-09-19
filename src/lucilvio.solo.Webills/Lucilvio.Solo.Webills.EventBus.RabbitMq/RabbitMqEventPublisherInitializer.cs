using System;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.EventBus.RabbitMq
{
    public static class RabbitMqEventPublisherInitializer
    {
        public static IEventPublisher AddRabbitMqEventPublisher(this IServiceCollection services,
            Configurations configurations)
        {
            var eventPublisher = new RabbitMqEventPublisher(configurations);

            services.AddSingleton(eventPublisher);
            return eventPublisher;
        }

        public static IEventPublisher AddRabbitMqEventPublisher(this IServiceCollection services,
            Action<Configurations> configs)
        {
            var configurations = new Configurations();
            configs(configurations);

            var eventPublisher = new RabbitMqEventPublisher(configurations);

            services.AddSingleton(eventPublisher);
            return eventPublisher;
        }
    }
}
