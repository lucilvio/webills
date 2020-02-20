using System;

using Lucilvio.Solo.Webills.Transactions.Domain;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess
{
    internal class TransactionsContext : DbContext
    {
        public TransactionsContext()
        {
            base.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300; ", opt =>
            {
                opt.MigrationsHistoryTable($"__Transactions_MigrationsHistory", "Transactions");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>(e =>
            {
                e.ToTable("Expenses", "Transactions");

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.Category).IsRequired();
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));

                e.HasOne<User>().WithMany(u => u.Expenses).IsRequired();
            });

            modelBuilder.Entity<Income>(i =>
            {
                i.ToTable("Incomes", "Transactions");

                i.HasKey("Id");
                i.Property<Guid>("Id").ValueGeneratedNever();

                i.Property(p => p.Id).IsRequired();
                i.Property(p => p.Date).IsRequired();
                i.Property(p => p.Name).IsRequired().HasMaxLength(256);
                i.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));

                i.HasOne<User>().WithMany(u => u.Incomes).IsRequired();
            });

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users", "Transactions");

                u.HasKey(u => u.Id);
                u.Property(p => p.Id).ValueGeneratedNever();
            });
        }

        public DbSet<User> Users { get; internal set; }
    }
}