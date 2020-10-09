using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    class DefaultEventBus : IEventBus
    {
        private readonly IDictionary<Module.Events, BusEvent> _busEventsMap;

        public DefaultEventBus()
        {
            this._busEventsMap = new Dictionary<Module.Events, BusEvent>
            {
                { Module.Events.OnLogin, new BusEvent() }
            };
        }

        public void Publish(Module.Events @event, object input)
        {
            if (!this._busEventsMap.TryGetValue(@event, out var busEvent))
                return;

            busEvent.Raise(input);
        }

        public void Subscibe(Module.Events @event, Func<object, Task> reaction)
        {
            if (!this._busEventsMap.TryGetValue(@event, out var busEvent))
                return;

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