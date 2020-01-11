﻿// <auto-generated />
using System;
using Lucilvio.Solo.Webills.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lucilvio.Solo.Webills.Web.Migrations
{
    [DbContext(typeof(WebillsContext))]
    partial class WebillsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.Profile.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("TermsAccepted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.Security.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.User.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.User.Income", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Number")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.Security.User.User", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Domain.Profile.User.User", null)
                        .WithOne()
                        .HasForeignKey("Lucilvio.Solo.Webills.Domain.Security.User.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.User.Expense", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Domain.User.User", null)
                        .WithMany("Expenses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.User.Income", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Domain.User.User", null)
                        .WithMany("Incomes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Domain.User.User", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Domain.Profile.User.User", null)
                        .WithOne()
                        .HasForeignKey("Lucilvio.Solo.Webills.Domain.User.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
