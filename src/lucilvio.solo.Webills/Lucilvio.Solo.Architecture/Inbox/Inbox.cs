using System;
using System.Threading.Tasks;
using System.Transactions;
using Lucilvio.Solo.Webills.Notification.Infrastructure.Inbox;

namespace Lucilvio.Solo.Architecture.Inbox
{
    public class Inbox<TMessage> : IHandler<TMessage> where TMessage : Message
    {
        private readonly Event _event;
        private readonly IHandler<TMessage> _innerHandler;
        private readonly IInboxDataAccess _dataAccess;

        public Inbox(Event @event, IHandler<TMessage> innerHandler, IInboxDataAccess dataAccess)
        {
            this._event = @event;
            this._innerHandler = innerHandler ?? throw new ArgumentNullException(nameof(innerHandler));
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(TMessage message)
        {
            try
            {
                if (await this._dataAccess.EventHasBeenReceived(this._event.Id))
                    return;

                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var newIncomingEvent = new IncomingEvent(this._event.Id, this._event.Name, this._event.Sender);
                await this._dataAccess.PersistIncomingEvent(newIncomingEvent);

                await this._innerHandler.Execute(message);

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

    public interface IInboxDataAccess
    {
        Task<bool> EventHasBeenReceived(Guid id);
        Task PersistIncomingEvent(IncomingEvent incomingEvent);
        Task UpdateEventStatus(IncomingEvent newIncomingEvent);
    }

    //internal class InboxDataAccess : IInboxDataAccess
    //{
    //    private readonly NotificationDataContext _context;

    //    public InboxDataAccess(NotificationDataContext context)
    //    {
    //        this._context = context ?? throw new ArgumentNullException(nameof(context));
    //    }

    //    public async Task<bool> EventHasBeenReceived(Guid id)
    //    {
    //        return await this._context.IncomingEvents.FirstOrDefaultAsync(ie => ie.Id == id) != null;
    //    }

    //    public async Task PersistIncomingEvent(IncomingEvent incomingEvent)
    //    {
    //        await this._context.IncomingEvents.AddAsync(incomingEvent);
    //        await this._context.SaveChangesAsync();
    //    }

    //    public async Task UpdateEventStatus(IncomingEvent newIncomingEvent)
    //    {
    //        this._context.Entry(newIncomingEvent).State = EntityState.Modified;
    //        await this._context.SaveChangesAsync();
    //    }
    //}
}