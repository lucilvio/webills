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

                e.Property(p => p.Name);
                e.Property(p => p.Date);    
                e.Property(p => p.Value).HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<Income>(i =>
            {
                i.ToTable("Incomes");
                i.Property<int>("Id").ValueGeneratedOnAdd();
                i.HasKey("Id");

                i.Property(p => p.Name);
                i.Property(p => p.Date);
                i.Property(p => p.Number).HasColumnName("Number");
                i.Property(p => p.Value).HasConversion(v => v.Value, v => new TransactionValue(v));
            });

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users");
                u.Property<int>("Id").ValueGeneratedOnAdd();
                u.HasKey("Id");

                u.Property(p => p.Name);
            });
        }

        public DbSet<User> Users { get; internal set; }
    }
}