using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Savings.infraestructure.dataaccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "savings");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "savings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccounts",
                schema: "savings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingsAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "savings",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "savings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    SavingsAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_SavingsAccounts_SavingsAccountId",
                        column: x => x.SavingsAccountId,
                        principalSchema: "savings",
                        principalTable: "SavingsAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccounts_UserId",
                schema: "savings",
                table: "SavingsAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SavingsAccountId",
                schema: "savings",
                table: "Transactions",
                column: "SavingsAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "savings");

            migrationBuilder.DropTable(
                name: "SavingsAccounts",
                schema: "savings");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "savings");
        }
    }
}
