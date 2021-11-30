using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component.Infrastructure
{
    internal class OutboxDataAccess
    {
        private readonly DbContext _dbContext;

        public OutboxDataAccess(DbContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task PersistEvent(OutgoingEvent outgoingEvent)
        {
            var dbSet = this._dbContext.Set<OutgoingEvent>();
            dbSet.Add(outgoingEvent);

            await this._dbContext.SaveChangesAsync();
        }
    }

    public static class OutboxModelBuilder
    {
        public static void MapOutboxModel(this ModelBuilder modelBuilder, string schema, string table = "OutgoingEvents")
        {
            modelBuilder.Entity<OutgoingEvent>(o =>
            {
                o.ToTable(table, schema);

                o.HasKey(p => p.Id);
                o.Property(p => p.Id).ValueGeneratedNever();

                o.Property(p => p.Name).IsRequired().HasMaxLength(256);
                o.Property(p => p.Sender).IsRequired().HasMaxLength(256);
                o.Property(p => p.Payload).IsRequired().HasMaxLength(2048);
                o.Property(p => p.Date).IsRequired();
            });
        }
    }
}