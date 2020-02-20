using Lucilvio.Solo.Webills.UserProfile.Domain;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserProfile.DataStorage
{
    class UserProfileContext : DbContext
    {
        private readonly string _connectionString;

        internal UserProfileContext(string connectionString, DbContextOptions<UserProfileContext> options) : base(options)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapUser(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public static void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
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

        public DbSet<User> Users { get; internal set; }
    }
}