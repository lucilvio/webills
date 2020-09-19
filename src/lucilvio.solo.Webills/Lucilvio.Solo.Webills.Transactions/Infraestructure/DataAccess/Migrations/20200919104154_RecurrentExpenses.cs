using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess.Migrations
{
    public partial class RecurrentExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "transactions");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Transactions",
                newName: "Users",
                newSchema: "transactions");

            migrationBuilder.RenameTable(
                name: "Incomes",
                schema: "Transactions",
                newName: "Incomes",
                newSchema: "transactions");

            migrationBuilder.RenameTable(
                name: "Expenses",
                schema: "Transactions",
                newName: "Expenses",
                newSchema: "transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "RecurrentExpenseId",
                schema: "transactions",
                table: "Expenses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecurrentExpenses",
                schema: "transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Until = table.Column<DateTime>(nullable: true),
                    Frequency = table.Column<int>(nullable: true),
                    RepetitionCount = table.Column<int>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrentExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurrentExpenses_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "transactions",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_RecurrentExpenseId",
                schema: "transactions",
                table: "Expenses",
                column: "RecurrentExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrentExpenses_UserId",
                schema: "transactions",
                table: "RecurrentExpenses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_RecurrentExpenses_RecurrentExpenseId",
                schema: "transactions",
                table: "Expenses",
                column: "RecurrentExpenseId",
                principalSchema: "transactions",
                principalTable: "RecurrentExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_RecurrentExpenses_RecurrentExpenseId",
                schema: "transactions",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "RecurrentExpenses",
                schema: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_RecurrentExpenseId",
                schema: "transactions",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RecurrentExpenseId",
                schema: "transactions",
                table: "Expenses");

            migrationBuilder.EnsureSchema(
                name: "Transactions");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "transactions",
                newName: "Users",
                newSchema: "Transactions");

            migrationBuilder.RenameTable(
                name: "Incomes",
                schema: "transactions",
                newName: "Incomes",
                newSchema: "Transactions");

            migrationBuilder.RenameTable(
                name: "Expenses",
                schema: "transactions",
                newName: "Expenses",
                newSchema: "Transactions");
        }
    }
}
