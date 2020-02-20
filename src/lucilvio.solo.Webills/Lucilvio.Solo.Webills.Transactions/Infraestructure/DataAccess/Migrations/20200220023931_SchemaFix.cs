using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess.Migrations
{
    public partial class SchemaFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Transactions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "Transactions");

            migrationBuilder.RenameTable(
                name: "Incomes",
                newName: "Incomes",
                newSchema: "Transactions");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "Expenses",
                newSchema: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Transactions",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Incomes",
                schema: "Transactions",
                newName: "Incomes");

            migrationBuilder.RenameTable(
                name: "Expenses",
                schema: "Transactions",
                newName: "Expenses");
        }
    }
}
