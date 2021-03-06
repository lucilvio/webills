﻿// <auto-generated />
using System;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lucilvio.Solo.Webills.UserAccount.infraestructure.dataAccess.Migrations
{
    [DbContext(typeof(UserAccountDataContext))]
    [Migration("20210315183722_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lucilvio.Solo.Webills.UserAccount.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("TermAccepted")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts", "UserAccount");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8b98ad31-334d-4aa3-b86e-73e1ff3fbf95"),
                            Login = "admin@mail.com",
                            Password = "7C4A8D09CA3762AF61E59520943DC26494F8941B",
                            TermAccepted = true,
                            UserId = new Guid("bbfdc67b-c844-4a9d-a1d6-64967a5a98f9")
                        });
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.UserAccount.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Users", "UserAccount");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bbfdc67b-c844-4a9d-a1d6-64967a5a98f9"),
                            Email = "admin@mail.com",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.UserAccount.Domain.Account", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.UserAccount.Domain.User", null)
                        .WithOne("Account")
                        .HasForeignKey("Lucilvio.Solo.Webills.UserAccount.Domain.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.UserAccount.Domain.User", b =>
                {
                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
