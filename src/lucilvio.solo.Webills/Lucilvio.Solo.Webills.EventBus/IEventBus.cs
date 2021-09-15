using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.EventBus
{
    public interface IEventBus
    {
        Task Publish(Event @event);
        Task Subscribe(string eventName, Func<Event, Task> handleEvent);
    }

    public record Event
    {
        public Event(string name, string sender, object payload)
        {
            this.Id = Guid.NewGuid();

            this.Name = name;
            this.Sender = sender;
            this.Payload = JsonSerializer.Serialize(payload);
        }

        public Guid Id { get; set; }
        public string Name { get; }
        public string Sender { get; }
        public string Payload { get; }
    }
}