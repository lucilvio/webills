using Lucilvio.Solo.Architecture.Inbox;
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

            base.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString, opt =>
            {
                opt.MigrationsHistoryTable($"__MigrationsHistory", this._schema);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapInboxModel(this._schema);

            base.OnModelCreating(modelBuilder);
        }
    }
}