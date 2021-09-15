using System;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure
{
    internal record OutgoingEvent
    {
        public OutgoingEvent(Guid id, string name, string sender, string payload)
        {
            this.Id = id;
            this.Name = name;
            this.Sender = sender;
            this.Payload = payload;

            this.Date = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Sender { get; }
        public DateTime Date { get; }
        public string Payload { get; }
    }
}