using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component.Infrastructure
{
    internal class OutboxDataAccess
    {
        private readonly OutboxDataContext _dbContext;

        public OutboxDataAccess(OutboxDataContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task PersistEvent(OutgoingEvent outgoingEvent)
        {
            this._dbContext.OutgoingEvents.Add(outgoingEvent);
            await this._dbContext.SaveChangesAsync();
        }
    }
}