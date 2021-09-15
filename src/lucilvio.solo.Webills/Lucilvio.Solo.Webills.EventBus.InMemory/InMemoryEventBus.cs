using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.EventBus.InMemory
{
    public class InMemoryEventBus : IEventBus
    {
        private static IDictionary<string, Func<Event, Task>> subscribers;

        public InMemoryEventBus()
        {
            subscribers = new Dictionary<string, Func<Event, Task>>();
        }

        public async Task Publish(Event @event)
        {
            foreach (var subscriber in subscribers)
            {
                Task.Run(() =>
                {
                    subscriber.Value(@event);
                });
            }
        }

        public async Task Subscribe(string eventName, Func<Event, Task> handleEvent)
        {
            subscribers.Add(eventName, handleEvent);
        }
    }
}