using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.Web
{
    public class WebillsContext : DbContext
    {
        public WebillsContext(DbContextOptions options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>(e =>
            {
                e.ToTable("Expenses");
                e.Property<int>("Id").ValueGeneratedOnAdd();
                e.HasKey("Id");

                e.Property(p => p.Name).IsRequired().HasMaxLength(256);
                e.Property(p => p.Date).IsRequired();
                e.Property(p => p.Number).IsRequired();
                e.Property(p => p.Category).IsRequired();
                e.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<Income>(i =>
            {
                i.ToTable("Incomes");
                i.Property<int>("Id").ValueGeneratedOnAdd();
                i.HasKey("Id");

                i.Property(p => p.Name).IsRequired().HasMaxLength(256);
                i.Property(p => p.Date).IsRequired();
                i.Property(p => p.Number).IsRequired().HasColumnName("Number");
                i.Property(p => p.Value).IsRequired().HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users");
                u.Property<int>("Id").ValueGeneratedOnAdd();
                u.HasKey("Id");

                u.Property(p => p.Name).IsRequired().HasMaxLength(256);
                u.Property(p => p.Login).IsRequired().HasMaxLength(256).HasConversion(l => l.Value, l => new Login(l));
                u.Property(p => p.Password).IsRequired().HasMaxLength(256).HasConversion(p => p.Value, p => new Password(p));
            });
        }

        public DbSet<User> Users { get; internal set; }
    }
}