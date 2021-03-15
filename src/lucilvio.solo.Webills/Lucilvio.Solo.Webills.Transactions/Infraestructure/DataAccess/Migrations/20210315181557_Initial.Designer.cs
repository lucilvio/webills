﻿// <auto-generated />
using System;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lucilvio.Solo.Webills.FinancialControl.infraestructure.dataAccess.Migrations
{
    [DbContext(typeof(FinancialControlDataContext))]
    [Migration("20210315181557_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lucilvio.Solo.Webills.FinancialControl.Domain.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid?>("RecurrentExpenseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("RecurrentExpenseId");

                    b.ToTable("Expenses", "FinancialControl");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.FinancialControl.Domain.Income", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Incomes", "FinancialControl");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.FinancialControl.Domain.RecurrentExpense", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RecurrentExpenses", "FinancialControl");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.FinancialControl.Domain.Expense", b =>
                {
                    b.HasOne("Lucilvio.Solo.Webills.FinancialControl.Domain.RecurrentExpense", null)
                        .WithMany("Expenses")
                        .HasForeignKey("RecurrentExpenseId");
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.FinancialControl.Domain.RecurrentExpense", b =>
                {
                    b.OwnsOne("Lucilvio.Solo.Webills.FinancialControl.Domain.Recurrency", "Recurrency", b1 =>
                        {
                            b1.Property<Guid>("RecurrentExpenseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Frequency")
                                .HasColumnType("int")
                                .HasColumnName("Frequency");

                            b1.Property<DateTime>("Until")
                                .HasColumnType("datetime2")
                                .HasColumnName("Until");

                            b1.HasKey("RecurrentExpenseId");

                            b1.ToTable("RecurrentExpenses");

                            b1.WithOwner()
                                .HasForeignKey("RecurrentExpenseId");
                        });

                    b.Navigation("Recurrency")
                        .IsRequired();
                });

            modelBuilder.Entity("Lucilvio.Solo.Webills.FinancialControl.Domain.RecurrentExpense", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
