using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Architecture.EventPublisher.InMemory
{
    internal class InMemoryEventPublisher : IEventPublisher
    {
        private readonly IServiceCollection _services;
        private IServiceProvider _provider;

        public InMemoryEventPublisher(IServiceCollection services)
        {
            this._services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public async Task Publish(Event @event)
        {
            if (this._provider is null)
                this._services.BuildServiceProvider();

            this._provider = this._services.BuildServiceProvider();
            var modules = this._provider.GetServices<IModule>();

            Task.Run(() =>
            {
                foreach (var module in modules)
                {
                    module.HandleEvent(@event);
                }
            });
        }
    }
}