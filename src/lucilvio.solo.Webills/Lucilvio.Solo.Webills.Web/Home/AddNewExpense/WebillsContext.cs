using System;
using Lucilvio.Solo.Webills.Domain.Profile.User;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web
{
    public class WebillsContext : DbContext
    {
        public WebillsContext(DbContextOptions<WebillsContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>(e =>
            {
                e.ToTable("Expenses");

                e.HasKey("Id");
                e.Property<Guid>("Id").ValueGeneratedNever();

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.Category).IsRequired();
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));

                
            });

            modelBuilder.Entity<Income>(i =>
            {
                i.ToTable("Incomes");

                i.HasKey("Id");
                i.Property<Guid>("Id").ValueGeneratedNever();

                i.Property(p => p.Name).IsRequired().HasMaxLength(256);
                i.Property(p => p.Date).IsRequired();
                i.Property(p => p.Id).IsRequired().HasColumnName("Number");
                i.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<Domain.User.User>(u =>
            {
                u.ToTable("Users");
                u.HasKey(u => u.Id);
                u.Ignore(u => u.Name);

                u.HasOne<Domain.Profile.User.User>().WithOne().HasForeignKey<Domain.User.User>(e => e.Id);
            });

            modelBuilder.Entity<Domain.Security.User.User>(u =>
            {
                u.ToTable("Users");
                u.HasKey(u => u.Id);
                u.Ignore(u => u.Name);
                
                u.HasOne<Domain.Profile.User.User>().WithOne().HasForeignKey<Domain.Security.User.User>(e => e.Id);
            });

            modelBuilder.Entity<Domain.Profile.User.User>(u =>
            {
                u.ToTable("Users");

                u.HasKey(p => p.Id);
                u.Property(p => p.Id).ValueGeneratedNever();

                u.Property(p => p.Name).IsRequired().HasMaxLength(256);
                u.Property(p => p.Login).IsRequired().HasMaxLength(256).HasConversion(l => l.Value, l => new Login(l));
                u.Property(p => p.Password).IsRequired().HasMaxLength(256).HasConversion(p => p.Value, p => new Password(p));
                u.Property(p => p.TermsAccepted).IsRequired();

            });
        }

        public DbSet<Domain.Profile.User.User> Users { get; set; }
    }
}