using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Architecture.Handler.Inbox.Component.Infrastructure
{
    internal class InboxDataContext : DbContext
    {
        private readonly string _schema;
        private readonly string _table;
        private readonly string _connectionString;

        public InboxDataContext(string connectionString, string schema, string table)
        {
            this._schema = schema;
            this._table = table;
            this._connectionString = connectionString;

            base.Database.Migrate();
        }

        internal DbSet<IncomingEvent> IncomingEvents { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString, opt =>
            {
                opt.MigrationsHistoryTable("__MigrationsHistory", this._schema);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.MapInboxModel(modelBuilder);
        }

        private void MapInboxModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomingEvent>(i =>
            {
                i.ToTable(this._table, this._schema);

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
