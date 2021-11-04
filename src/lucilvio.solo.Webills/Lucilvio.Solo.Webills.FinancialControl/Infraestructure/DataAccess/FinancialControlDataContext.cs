using System;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess
{
    internal class FinancialControlDataContext : DbContext
    {
        private readonly string _schema;
        private readonly string _connectionString;

        public FinancialControlDataContext(Configurations configurations)
        {
            this._schema = configurations.ModuleName;
            this._connectionString = configurations.DataConnectionString;

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>(e =>
            {
                e.ToTable("Expenses", this._schema);

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.Property(p => p.UserId).IsRequired();
                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.Category).IsRequired();
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<Income>(i =>
            {
                i.ToTable("Incomes", this._schema);

                i.HasKey("Id");
                i.Property<Guid>("Id").ValueGeneratedNever();

                i.Property(p => p.UserId).IsRequired();
                i.Property(p => p.Id).IsRequired();
                i.Property(p => p.Date).IsRequired();
                i.Property(p => p.Name).IsRequired().HasMaxLength(256);
                i.Property(p => p.Category).IsRequired();
                i.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<RecurrentExpense>(e =>
            {
                e.ToTable("RecurrentExpenses", this._schema);

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.OwnsOne(p => p.Recurrency, r =>
                {
                    r.Property(p => p.Until).IsRequired().HasColumnName("Until");
                    r.Property(p => p.Frequency).IsRequired().HasColumnName("Frequency").HasConversion(p => p.Value, p => Frequency.FromValue(p));
                });

                e.Navigation(p => p.Recurrency).IsRequired();

                e.HasMany(p => p.Expenses).WithOne().HasForeignKey(e => e.RecurrentExpenseId);
            });

            modelBuilder.Entity<RecurrentIncome>(e =>
            {
                e.ToTable("RecurrentIncomes", this._schema);

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.OwnsOne(p => p.Recurrency, r =>
                {
                    r.Property(p => p.Until).IsRequired().HasColumnName("Until");
                    r.Property(p => p.Frequency).IsRequired().HasColumnName("Frequency").HasConversion(p => p.Value, p => Frequency.FromValue(p));
                });

                e.Navigation(p => p.Recurrency).IsRequired();

                e.HasMany(p => p.Incomes).WithOne().HasForeignKey(e => e.RecurrentIncomeId);
            });
        }

        public DbSet<Income> Incomes { get; internal set; }
        public DbSet<Expense> Expenses { get; internal set; }
    }
}