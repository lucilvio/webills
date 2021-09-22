using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Lucilvio.Solo.Architecture.Inbox
{
    public class Inbox<TMessage> where TMessage : Message
    {
        private readonly IInboxDataAccess _dataAccess;
        private readonly IHandler<TMessage> _eventHandler;

        public Inbox(IInboxDataAccess dataAccess, IHandler<TMessage> eventHandler)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._eventHandler = eventHandler ?? throw new ArgumentNullException(nameof(eventHandler));
        }

        public async Task Execute(TMessage message, Event @event)
        {
            try
            {
                if (await this._dataAccess.EventHasBeenReceived(@event.Id))
                    return;

                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var newIncomingEvent = new IncomingEvent(@event.Id, @event.Name, @event.Sender);
                await this._dataAccess.PersistIncomingEvent(newIncomingEvent);

                await this._eventHandler.Execute(message);

                newIncomingEvent.MarkAsProcessed();

                await this._dataAccess.UpdateEventStatus(newIncomingEvent);

                transaction.Complete();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}