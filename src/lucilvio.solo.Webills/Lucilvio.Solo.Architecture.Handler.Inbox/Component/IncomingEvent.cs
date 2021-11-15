using System;

namespace Lucilvio.Solo.Architecture.Handler.Inbox.Component
{
    internal class IncomingEvent
    {
        public IncomingEvent(Guid id, string name, string sender)
        {
            this.Id = id;
            this.Name = name;
            this.Sender = sender;

            this.Date = DateTime.UtcNow;
            this.Status = EventStatus.Created;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Sender { get; }
        public DateTime Date { get; }
        public EventStatus Status { get; private set; }

        public enum EventStatus
        {
            Created = 1,
            Processed = 2
        }

        internal void MarkAsProcessed()
        {
            this.Status = EventStatus.Processed;
        }
    }
}