using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess.Migrations
{
    public partial class RelationFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_Id",
                schema: "Transactions",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_Id",
                schema: "Transactions",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId",
                schema: "Transactions",
                table: "Incomes");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Transactions",
                table: "Incomes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Transactions",
                table: "Expenses",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                schema: "Transactions",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                schema: "Transactions",
                table: "Expenses",
                column: "UserId",
                principalSchema: "Transactions",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId",
                schema: "Transactions",
                table: "Incomes",
                column: "UserId",
                principalSchema: "Transactions",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                schema: "Transactions",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId",
                schema: "Transactions",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId",
                schema: "Transactions",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Transactions",
                table: "Expenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Transactions",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_Id",
                schema: "Transactions",
                table: "Expenses",
                column: "Id",
                principalSchema: "Transactions",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_Id",
                schema: "Transactions",
                table: "Incomes",
                column: "Id",
                principalSchema: "Transactions",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
