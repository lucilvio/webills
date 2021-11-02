using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Architecture.EventPublisher.InMemory
{
    public static class InMemoryEventPublisherInitializer
    {
        public static IEventPublisher AddInMemoryEventPublisher(this IServiceCollection services)
        {
            var eventPublisher = new InMemoryEventPublisher(services);
            services.AddSingleton(eventPublisher);

            return eventPublisher;
        }
    }
}
