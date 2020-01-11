using Microsoft.EntityFrameworkCore.Migrations;

namespace Lucilvio.Solo.Webills.Web.Migrations
{
    public partial class boundedContexts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_Id",
                schema: "core",
                table: "Users",
                column: "Id",
                principalSchema: "profile",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_Id",
                schema: "security",
                table: "Users",
                column: "Id",
                principalSchema: "profile",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_Id",
                schema: "core",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_Id",
                schema: "security",
                table: "Users");
        }
    }
}
