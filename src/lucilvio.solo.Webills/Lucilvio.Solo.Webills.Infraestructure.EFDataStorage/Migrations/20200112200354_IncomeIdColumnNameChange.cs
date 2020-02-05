using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Migrations
{
    public partial class IncomeIdColumnNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Incomes",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Incomes",
                newName: "Number");
        }
    }
}
