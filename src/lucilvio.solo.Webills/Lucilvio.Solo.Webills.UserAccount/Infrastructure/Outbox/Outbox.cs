using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure
{
    internal class Outbox : IEventBus
    {
        private readonly IEventBus _innerEventBus;
        private readonly IOutboxDataAccess _dataAccess;

        public Outbox(IEventBus innerEventBus, IOutboxDataAccess dataAccess)
        {
            this._innerEventBus = innerEventBus ?? throw new ArgumentNullException(nameof(innerEventBus));
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Publish(Event @event)
        {
            await this._dataAccess.PersistEvent(new OutgoingEvent(@event.Id, @event.Name, @event.Sender, @event.Payload));
            await this._innerEventBus.Publish(@event);
        }

        public async Task Subscribe(string eventName, Func<Event, Task> handleEvent)
        {
            await this._innerEventBus.Subscribe(eventName, handleEvent);
        }
    }

    internal interface IOutboxDataAccess
    {
        Task PersistEvent(OutgoingEvent outgoingEvent);
    }

    internal class OutboxDataAccess : IOutboxDataAccess
    {
        private readonly UserAccountDataContext _context;

        public OutboxDataAccess(UserAccountDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task PersistEvent(OutgoingEvent outgoingEvent)
        {
            await this._context.OutgoingEvents.AddAsync(outgoingEvent);
            await this._context.SaveChangesAsync();
        }
    }
}
