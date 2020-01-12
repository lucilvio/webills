using System;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage
{
    public class WebillsContext : DbContext
    {
        public WebillsContext(DbContextOptions<WebillsContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Webills.Core.Domain.User.Expense>(e =>
            {
                e.ToTable("Expenses");

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.Category).IsRequired();
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new Webills.Core.Domain.User.TransactionValue(v));

                
            });

            modelBuilder.Entity<Webills.Core.Domain.User.Income>(e =>
            {
                e.ToTable("Incomes");

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Id).IsRequired().HasColumnName("Number");
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new Webills.Core.Domain.User.TransactionValue(v));
            });

            modelBuilder.Entity<Webills.Core.Domain.User.User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.Id);
                e.Ignore(u => u.Name);

                e.HasOne<Webills.Profile.Domain.User.User>().WithOne().HasForeignKey<Webills.Core.Domain.User.User>(e => e.Id);
            });

            modelBuilder.Entity<Webills.Security.Domain.User.User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.Id);
                e.Ignore(u => u.Name);
                
                e.HasOne<Webills.Profile.Domain.User.User>().WithOne().HasForeignKey<Webills.Security.Domain.User.User>(e => e.Id);
            });

            modelBuilder.Entity<Webills.Profile.Domain.User.User>(e =>
            {
                e.ToTable("Users");

                e.HasKey(p => p.Id);
                e.Property(p => p.Id).ValueGeneratedNever();

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Login).IsRequired().HasMaxLength(256).HasConversion(l => l.Value, l => new Webills.Profile.Domain.User.Login(l));
                e.Property(p => p.Password).IsRequired().HasMaxLength(256).HasConversion(p => p.Value, p => new Webills.Profile.Domain.User.Password(p));
                e.Property(p => p.TermsAccepted).IsRequired();

            });
        }

        public DbSet<Webills.Profile.Domain.User.User> Users { get; set; }
    }
}