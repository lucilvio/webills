using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.Security.Domain.User;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Security
{
    public class WebillsSecurityContext : DbContext
    {
        public WebillsSecurityContext(DbContextOptions<WebillsSecurityContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            {
                u.HasNoKey();
                u.ToTable("Users");
            });
        }

        public DbSet<User> Users { get; internal set; }
    }
}