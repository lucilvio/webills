using Lucilvio.Solo.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.EventBus.InMemory
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
