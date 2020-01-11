using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.Domain.Profile.User;

namespace Lucilvio.Solo.Webills.Web
{
    public class WebillsProfileContext : DbContext
    {
        public WebillsProfileContext(DbContextOptions<WebillsProfileContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

        public DbSet<Domain.Profile.User.User> Users { get; internal set; }
    }
}