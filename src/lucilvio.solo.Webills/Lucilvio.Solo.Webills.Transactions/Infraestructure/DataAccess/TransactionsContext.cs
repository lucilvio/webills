using System;

using Lucilvio.Solo.Webills.Transactions.Domain;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess
{
    internal class TransactionsContext : DbContext
    {
        private readonly string _connectionString;

        public TransactionsContext(string connectionString)
        {
            this._connectionString = connectionString;
            base.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString, opt =>
            {
                opt.MigrationsHistoryTable($"__Transactions_MigrationsHistory", "transactions");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var schema = "transactions";


            modelBuilder.Entity<Expense>(e =>
            {
                e.ToTable("Expenses", schema);

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.Category).IsRequired();
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));

                e.HasOne<User>().WithMany(u => u.Expenses).IsRequired();
                e.HasOne<RecurrentExpense>().WithMany(r => r.Expenses);
            });

            modelBuilder.Entity<Income>(i =>
            {
                i.ToTable("Incomes", schema);

                i.HasKey("Id");
                i.Property<Guid>("Id").ValueGeneratedNever();

                i.Property(p => p.Id).IsRequired();
                i.Property(p => p.Date).IsRequired();
                i.Property(p => p.Name).IsRequired().HasMaxLength(256);
                i.Property(p => p.Category).IsRequired();
                i.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));

                i.HasOne<User>().WithMany(u => u.Incomes).IsRequired();
            });

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users", schema);

                u.HasKey(u => u.Id);
                u.Property(p => p.Id).ValueGeneratedNever();
            });
            
            modelBuilder.Entity<RecurrentExpense>(e =>
            {
                e.ToTable("RecurrentExpenses", schema);

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();
                
                e.OwnsOne(p => p.Recurrency).Property(p => p.RepetitionCount).IsRequired()
                    .HasColumnName(nameof(Recurrency.RepetitionCount));
                
                e.OwnsOne(p => p.Recurrency).Property(p => p.Until).IsRequired()
                    .HasColumnName(nameof(Recurrency.Until));
                
                e.OwnsOne(p => p.Recurrency).Property(p => p.Frequency).IsRequired()
                    .HasConversion(p => p.Value, p => Frequency.FromValue(p))
                    .HasColumnName(nameof(Recurrency.Frequency));

                e.HasOne<User>().WithMany(u => u.RecurrentExpenses).IsRequired();
            });
        }

        public DbSet<User> Users { get; internal set; }
    }
}