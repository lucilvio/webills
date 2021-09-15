using Lucilvio.Solo.Webills.Notification.Infrastructure.Inbox;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Notification.Infrastructure.DataAccess
{
    internal class NotificationDataContext : DbContext
    {
        private readonly string _connectionString;

        public string _schema { get; }

        public NotificationDataContext(string connectionString)
        {
            this._schema = "Notifications";
            this._connectionString = connectionString;

            //base.Database.Migrate();
        }

        internal DbSet<IncomingEvent> IncomingEvents { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString, opt =>
            {
                opt.MigrationsHistoryTable($"__MigrationsHistory", this._schema);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.MapIncomingEvent(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private void MapIncomingEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomingEvent>(o =>
            {
                o.ToTable("IncomingEvents", this._schema);

                o.HasKey(p => p.Id);
                o.Property(p => p.Id).ValueGeneratedNever();

                o.Property(p => p.Name).IsRequired().HasMaxLength(256);
                o.Property(p => p.Sender).IsRequired().HasMaxLength(256);
                o.Property(p => p.Date).IsRequired();
                o.Property(p => p.Status).IsRequired();
            });
        }
    }
}
