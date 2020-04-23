using Lucilvio.Solo.Webills.Savings.Domain;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Savings.Infraestructure.DataAccess
{
    internal class SavingsContext : DbContext
    {
        private readonly string _connectionString;

        internal SavingsContext()
        {
            base.Database.Migrate();
        }

        public SavingsContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString, opt =>
            {
                opt.MigrationsHistoryTable($"__Savings_MigrationsHistory", "savings");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users", "savings");

                u.HasKey(u => u.Id);
                u.Property(p => p.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SavingsAccount>(e =>
            {
                e.ToTable("SavingsAccounts", "savings");

                e.HasKey(e => e.Id);
                e.HasOne<User>().WithMany(u => u.SavingsAccounts).IsRequired();
            });

            modelBuilder.Entity<Transaction>(e =>
            {
                e.ToTable("Transactions", "savings");

                e.HasKey(e => e.Id);
                e.Property(e => e.Date).IsRequired();
                e.Property(e => e.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));

                e.HasOne<SavingsAccount>().WithMany("_transactions").IsRequired();
            });
        }

        public DbSet<User> Users { get; set; }
    }
}