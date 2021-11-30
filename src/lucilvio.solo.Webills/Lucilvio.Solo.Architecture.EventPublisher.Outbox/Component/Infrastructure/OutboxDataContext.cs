using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component.Infrastructure
{
    internal class OutboxDataContext : DbContext
    {
        private readonly string _schema;
        private readonly string _table;
        private readonly string _connectionString;

        public OutboxDataContext(string connectionString, string schema, string table)
        {
            this._schema = schema;
            this._table = table;
            this._connectionString = connectionString;

            base.Database.Migrate();
        }

        internal DbSet<OutgoingEvent> OutgoingEvents { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString, opt =>
            {
                opt.MigrationsHistoryTable($"__MigrationsHistory", this._schema);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.MapOutboxModel(modelBuilder);
        }

        private void MapOutboxModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutgoingEvent>(o =>
            {
                o.ToTable(this._table, this._schema);

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
