using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Bus
{
    public interface IBus
    {
        Task SendEvent(object evt);
        void Subscribe<TObjectSubscriber>(string eventType, Func<string, Task> action);
    }

    public class Bus : IBus
    {
        private readonly IList<Subscriber> _subscribers;

        public Bus()
        {
            this._subscribers = new List<Subscriber>();
        }

        public async Task SendEvent(object evt)
        {
            var subscribers = this._subscribers.Where(sb => sb.EventType == evt.GetType().Name.ToLower());

            foreach (var subscriber in subscribers)
                await subscriber.EventHandler.Invoke(JsonSerializer.Serialize(evt, evt.GetType()));
        }

        public void Subscribe<TObjectSubscriber>(string eventType, Func<string, Task> eventHandler)
        {
            if (!this._subscribers.Any(sb => sb.Source == typeof(TObjectSubscriber)))
            {
                this._subscribers.Add(new Subscriber(typeof(TObjectSubscriber), eventType, eventHandler));
                return;
            }

            if (!this._subscribers.Any(sb => sb.Source == typeof(TObjectSubscriber) && sb.EventType == eventType))
                this._subscribers.Add(new Subscriber(typeof(TObjectSubscriber), eventType, eventHandler));
        }
    }

    public class Subscriber
    {
        public Subscriber(Type source, string eventType, Func<string, Task> action)
        {
            this.Source = source;
            this.EventType = eventType.ToLower();
            this.EventHandler = action;
        }

        public Type Source { get; }
        public string EventType { get; }
        public Func<string, Task> EventHandler { get; }
    }
}
