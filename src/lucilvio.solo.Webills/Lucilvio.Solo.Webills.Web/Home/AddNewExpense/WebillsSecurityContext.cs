using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.Domain.Security.User;

namespace Lucilvio.Solo.Webills.Web
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

                //u.HasOne<Domain.Profile.User.User>().WithOne().HasForeignKey<Domain.Security.User.User>(e => e.Id);
            });
        }

        public DbSet<User> Users { get; internal set; }
    }
}