using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammingCompetitionService.Migrations
{
    public partial class RenameTasksColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Script",
                table: "Tasks",
                newName: "UserScript");

            migrationBuilder.RenameColumn(
                name: "OutputParam",
                table: "Tasks",
                newName: "Output");

            migrationBuilder.RenameColumn(
                name: "InputParam",
                table: "Tasks",
                newName: "MainScript");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserScript",
                table: "Tasks",
                newName: "Script");

            migrationBuilder.RenameColumn(
                name: "Output",
                table: "Tasks",
                newName: "OutputParam");

            migrationBuilder.RenameColumn(
                name: "MainScript",
                table: "Tasks",
                newName: "InputParam");
        }
    }
}
