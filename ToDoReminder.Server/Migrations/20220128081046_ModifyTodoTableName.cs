using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoReminder.Server.Migrations
{
    public partial class ModifyTodoTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todo");

            migrationBuilder.CreateTable(
                name: "todoreminder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reminder_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    create_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todoreminder", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todoreminder");

            migrationBuilder.CreateTable(
                name: "todo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    create_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reminder_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    update_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
