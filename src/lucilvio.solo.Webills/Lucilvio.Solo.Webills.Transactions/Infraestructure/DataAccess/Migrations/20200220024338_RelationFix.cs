using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess.Migrations
{
    public partial class RelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Transactions",
                table: "Incomes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                schema: "Transactions",
                table: "Incomes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId",
                schema: "Transactions",
                table: "Incomes",
                column: "UserId",
                principalSchema: "Transactions",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId",
                schema: "Transactions",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_UserId",
                schema: "Transactions",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Transactions",
                table: "Incomes");
        }
    }
}
