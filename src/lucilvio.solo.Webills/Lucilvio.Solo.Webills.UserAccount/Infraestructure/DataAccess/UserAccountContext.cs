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
        internal DbSet<Account> Accounts { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300; ", opt =>
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
            var user = new
            {
                Id = Guid.NewGuid(),
                Name = new Name("Administrator"),
                Email = new Email("admin@mail.com")
            };

            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<Account>().HasData(new
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
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

                u.Property(p => p.Name).IsRequired().HasMaxLength(256).HasConversion(n => n.Value, n => new Name(n));
                u.Property(p => p.Email).IsRequired().HasMaxLength(256).HasConversion(e => e.Value, e => new Email(e));
            });

            modelBuilder.Entity<Account>(a =>
            {
                a.ToTable("Accounts", "useraccount");

                a.HasKey(p => p.Id);
                a.Property(p => p.Id).ValueGeneratedNever();

                a.Property(p => p.Login).IsRequired().HasMaxLength(256).HasConversion(l => l.Value, l => new Domain.Login(l));
                a.Property(p => p.Password).IsRequired().HasMaxLength(256).HasConversion(p => p.Value, v => new Password(v));
                a.Property(p => p.TermAccepted).IsRequired();

                a.HasOne<User>().WithOne(u => u.Account).HasForeignKey<Account>("UserId").IsRequired();
            });
        }

    }
}