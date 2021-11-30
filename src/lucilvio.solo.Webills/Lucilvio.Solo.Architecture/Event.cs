using System;
using Newtonsoft.Json;

namespace Lucilvio.Solo.Architecture
{
    public class Event
    {
        public Event(string name, string sender, object payload)
        {
            this.Id = Guid.NewGuid();

            this.Name = name;
            this.Sender = sender;
            this.Payload = payload;
        }

        public Guid Id { get; set; }
        public string Name { get; }
        public string Sender { get; }
        public object Payload { get; }

        public string Serialize() => JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        });
    }
}