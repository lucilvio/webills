using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Outbox
{
    internal class Outbox : IEventPublisher
    {
        private readonly IEventPublisher _innerEventBus;
        private readonly IOutboxDataAccess _dataAccess;

        public Outbox(IEventPublisher innerEventBus, IOutboxDataAccess dataAccess)
        {
            this._innerEventBus = innerEventBus ?? throw new ArgumentNullException(nameof(innerEventBus));
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Publish(Event @event)
        {
            await this._dataAccess.PersistEvent(new OutgoingEvent(@event.Id, @event.Name, @event.Sender, @event.Serialize()));
            await this._innerEventBus.Publish(@event);
        }
    }

    internal interface IOutboxDataAccess
    {
        Task PersistEvent(OutgoingEvent outgoingEvent);
    }

    //internal class OutboxDataAccess : IOutboxDataAccess
    //{
    //    private readonly UserAccountDataContext _context;

    //    public OutboxDataAccess(UserAccountDataContext context)
    //    {
    //        this._context = context ?? throw new ArgumentNullException(nameof(context));
    //    }

    //    public async Task PersistEvent(OutgoingEvent outgoingEvent)
    //    {
    //        await this._context.OutgoingEvents.AddAsync(outgoingEvent);
    //        await this._context.SaveChangesAsync();
    //    }
    //}
}
