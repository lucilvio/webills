using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component
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
            try
            {
                await this._dataAccess.PersistEvent(new OutgoingEvent(@event.Id, @event.Name, @event.Sender, @event.Serialize()));
                await this._innerEventBus.Publish(@event);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}