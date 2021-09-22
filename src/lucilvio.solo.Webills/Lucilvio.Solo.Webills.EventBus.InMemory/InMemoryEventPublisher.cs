using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.EventBus.InMemory
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
            var modules = this._provider.GetServices<Module>();

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