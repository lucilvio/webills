using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.Notification
{
    public abstract class Module
    {
        protected readonly IEventBus _eventBus;        
        protected readonly Configurations _configurations;

        public Module(Configurations configurations, EventsToSubscribe eventsToSubscribe, IEventBus eventBus)
        {
            this._configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            this.SubscribeEvents(eventsToSubscribe);
        }

        protected abstract Task SubscribeEvents(EventsToSubscribe eventsToSubscribe);

        public record Configurations
        {
            public string DataConnectionString { get; init; }
        }

        public record EventsToSubscribe
        {
            private readonly string[] _events;

            public EventsToSubscribe(params string[] events)
            {
                this._events = events;
            }

            internal string[] Events => _events;
            internal bool HasEvents => this._events != null && this._events.Any();
        }

        public class Error : Exception
        {
            public Error() { } 
        }
    }
}