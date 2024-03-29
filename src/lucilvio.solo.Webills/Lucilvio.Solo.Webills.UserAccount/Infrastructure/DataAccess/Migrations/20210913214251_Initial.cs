﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure.DataAccess.Migrations
{
    partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserAccount");

            migrationBuilder.CreateTable(
                name: "OutgoingEvents",
                schema: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutgoingEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TermAccepted = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserAccount",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "UserAccount",
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { new Guid("02ce532e-888b-48ab-96c3-6d630b6157da"), "admin@mail.com", "Admin" });

            migrationBuilder.InsertData(
                schema: "UserAccount",
                table: "Accounts",
                columns: new[] { "Id", "Login", "Password", "TermAccepted", "UserId" },
                values: new object[] { new Guid("7a136a24-9ddc-4b8c-9405-3017cfa80fda"), "admin@mail.com", "7C4A8D09CA3762AF61E59520943DC26494F8941B", true, new Guid("02ce532e-888b-48ab-96c3-6d630b6157da") });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                schema: "UserAccount",
                table: "Accounts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "UserAccount");

            migrationBuilder.DropTable(
                name: "OutgoingEvents",
                schema: "UserAccount");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserAccount");
        }
    }
}
