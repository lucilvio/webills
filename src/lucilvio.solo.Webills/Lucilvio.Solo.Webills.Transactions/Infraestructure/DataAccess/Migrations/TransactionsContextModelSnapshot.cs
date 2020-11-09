﻿// <auto-generated />
using System;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess.Migrations
{
    [DbContext(typeof(TransactionsContext))]
    partial class TransactionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.Expense", b =>
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

                    b.Property<Guid?>("RecurrentExpenseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("RecurrentExpenseId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses","transactions");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.Income", b =>
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes","transactions");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.RecurrentExpense", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RecurrentExpenses","transactions");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users","transactions");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.Expense", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Transactions.Domain.RecurrentExpense", null)
                        .WithMany("Expenses")
                        .HasForeignKey("RecurrentExpenseId");

                    b.HasOne("Lucilvio.Solo.Webills.Transactions.Domain.User", null)
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.Income", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Transactions.Domain.User", null)
                        .WithMany("Incomes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.Transactions.Domain.RecurrentExpense", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.Transactions.Domain.User", null)
                        .WithMany("RecurrentExpenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Lucilvio.Solo.Webills.Transactions.Domain.Recurrency", "Recurrency", b1 =>
                        {
                            b1.Property<Guid>("RecurrentExpenseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Frequency")
                                .HasColumnName("Frequency")
                                .HasColumnType("int");

                            b1.Property<int>("RepetitionCount")
                                .HasColumnName("RepetitionCount")
                                .HasColumnType("int");

                            b1.Property<DateTime>("Until")
                                .HasColumnName("Until")
                                .HasColumnType("datetime2");

                            b1.HasKey("RecurrentExpenseId");

                            b1.ToTable("RecurrentExpenses");

                            b1.WithOwner()
                                .HasForeignKey("RecurrentExpenseId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
