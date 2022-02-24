using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoReminder.Server.Migrations
{
    public partial class UserColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "neckname",
                table: "user",
                newName: "nickname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "user",
                newName: "neckname");
        }
    }
}
