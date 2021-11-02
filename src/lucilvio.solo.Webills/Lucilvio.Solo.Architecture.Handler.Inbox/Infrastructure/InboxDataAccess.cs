using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture.Handler.Inbox;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Architecture.Inbox
{
    internal class InboxDataAccess : IInboxDataAccess
    {
        private readonly DbContext _context;

        public InboxDataAccess(DbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> EventHasBeenReceived(Guid id)
        {
            return await this._context.Set<IncomingEvent>().FirstOrDefaultAsync(ie => ie.Id == id) != null;
        }

        public async Task PersistIncomingEvent(IncomingEvent incomingEvent)
        {
            await this._context.Set<IncomingEvent>().AddAsync(incomingEvent);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateEventStatus(IncomingEvent newIncomingEvent)
        {
            this._context.Entry(newIncomingEvent).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }

    public static class InboxModelBuilder
    {
        public static void MapInboxModel(this ModelBuilder modelBuilder, string schema, string table = "IncomingEvents")
        {
            modelBuilder.Entity<IncomingEvent>(i =>
            {
                i.ToTable(table, schema);

                i.HasKey(p => p.Id);
                i.Property(p => p.Id).ValueGeneratedNever();

                i.Property(p => p.Name).IsRequired().HasMaxLength(256);
                i.Property(p => p.Sender).IsRequired().HasMaxLength(256);
                i.Property(p => p.Date).IsRequired();
                i.Property(p => p.Status).IsRequired();
            });
        }
    }
}