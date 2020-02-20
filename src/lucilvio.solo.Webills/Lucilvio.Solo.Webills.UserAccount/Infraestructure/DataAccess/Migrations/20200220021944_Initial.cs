using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserAccount");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Login = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: false),
                    TermAccepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "UserAccount",
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "Password", "TermAccepted" },
                values: new object[] { new Guid("3f474fb2-f6dc-42cd-ac11-5eba4c2b3cd9"), "admin@mail.com", "Administrator", "7C4A8D09CA3762AF61E59520943DC26494F8941B", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserAccount");
        }
    }
}
