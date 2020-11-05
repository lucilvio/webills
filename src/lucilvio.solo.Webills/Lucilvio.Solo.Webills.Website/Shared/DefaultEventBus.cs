using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.Website.Shared
{
    internal class DefaultEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<string, BusEvent> _busEventsMap;

        public DefaultEventBus()
        {
            this._busEventsMap = new ConcurrentDictionary<string, BusEvent>();
        }

        public void Publish(string @event, object input)
        {
            var busEvent = this._busEventsMap.GetOrAdd(@event, new BusEvent());

            busEvent.Raise(input);
        }

        public void Subscibe(string @event, Func<dynamic, Task> reaction)
        {
            var busEvent = this._busEventsMap.GetOrAdd(@event, new BusEvent());

            busEvent.AddReaction(reaction);
        }

        class BusEvent
        {
            private event Func<object, Task> _evt;

            public void AddReaction(Func<object, Task> reaction)
            {
                this._evt += reaction;
            }

            internal void Raise(object input)
            {
                this._evt?.Invoke(input);
            }
        }
    }
}