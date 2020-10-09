using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess.Migrations
{
    internal partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "useraccount");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "useraccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "useraccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(maxLength: 256, nullable: false),
                    TermAccepted = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "useraccount",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "useraccount",
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { new Guid("b0fef984-c7b8-431f-83fc-23b0de46555c"), "admin@mail.com", "Administrator" });

            migrationBuilder.InsertData(
                schema: "useraccount",
                table: "Accounts",
                columns: new[] { "Id", "Login", "Password", "TermAccepted", "UserId" },
                values: new object[] { new Guid("440f2c7d-726e-4f37-9ccc-f83f726a16da"), "admin@mail.com", "7C4A8D09CA3762AF61E59520943DC26494F8941B", true, new Guid("b0fef984-c7b8-431f-83fc-23b0de46555c") });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                schema: "useraccount",
                table: "Accounts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "useraccount");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "useraccount");
        }
    }
}
