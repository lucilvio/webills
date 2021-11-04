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

        public Task Publish(Event @event)
        {
            if (this._provider is null)
                this._services.BuildServiceProvider();

            this._provider = this._services.BuildServiceProvider();
            var eventLusteners = this._provider.GetServices<IEventListener>();

            Task.Run(() =>
            {
                foreach (var module in eventLusteners)
                {
                    module.ListenEvent(@event);
                }
            });

            return Task.CompletedTask;
        }
    }
}