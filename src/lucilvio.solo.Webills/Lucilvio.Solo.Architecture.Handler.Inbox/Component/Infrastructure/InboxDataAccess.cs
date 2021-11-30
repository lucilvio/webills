using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Architecture.Handler.Inbox.Component.Infrastructure
{
    internal class InboxDataAccess : IInboxDataAccess
    {
        private readonly InboxDataContext _context;

        public InboxDataAccess(InboxDataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> EventHasBeenReceived(Guid id)
        {
            return await this._context.IncomingEvents.FirstOrDefaultAsync(ie => ie.Id == id) != null;
        }

        public async Task PersistIncomingEvent(IncomingEvent incomingEvent)
        {
            try
            {
                this._context.IncomingEvents.Add(incomingEvent);
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UniqueEventConstraintException();
            }
        }

        public async Task UpdateEventStatus(IncomingEvent newIncomingEvent)
        {
            this._context.Entry(newIncomingEvent).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}