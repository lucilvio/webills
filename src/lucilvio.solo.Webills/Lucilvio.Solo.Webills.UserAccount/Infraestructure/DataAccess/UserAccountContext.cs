using System;

using Lucilvio.Solo.Webills.UserAccount.Domain;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess
{
    internal class UserAccountContext : DbContext
    {
        public UserAccountContext()
        {
            base.Database.Migrate();
        }

        internal DbSet<User> Users { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300; ", opt =>
            {
                opt.MigrationsHistoryTable($"__UserAccount_MigrationsHistory", "useraccount");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapUser(modelBuilder);
            Seed(modelBuilder);

        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new
            {
                Id = Guid.NewGuid(),
                Name = new Name("Administrator"),
                Login = new Domain.Login("admin@mail.com"),
                Password = new Sha1EncryptedPassword(new Password("123456")),
                TermAccepted = true
            });
        }

        private void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users", "useraccount");

                u.HasKey(p => p.Id);
                u.Property(p => p.Id).ValueGeneratedNever();

                u.Property(p => p.Name).IsRequired().HasMaxLength(256).HasConversion(p => p.Value, p => new Name(p));
                u.Property(p => p.Login).IsRequired().HasMaxLength(256).HasConversion(l => l.Value, v => new Domain.Login(v));
                u.Property(p => p.Password).IsRequired().HasMaxLength(256).HasConversion(p => p.Value, v => new Password(v));
                u.Property(p => p.TermAccepted).IsRequired();
            });
        }

    }
}