using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammingCompetitionService.Migrations
{
    public partial class AddTaskDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "MainScript",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Output",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserScript",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ProgrammingLanguage",
                table: "CompletedTasks",
                newName: "Language");

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    TaskDetailsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TaskItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MainScript = table.Column<string>(type: "TEXT", nullable: false),
                    Output = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    UserScript = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.TaskDetailsId);
                    table.ForeignKey(
                        name: "FK_TaskDetails_Tasks_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "Tasks",
                        principalColumn: "TaskItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskItemId",
                table: "TaskDetails",
                column: "TaskItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.RenameColumn(
                name: "Language",
                table: "CompletedTasks",
                newName: "ProgrammingLanguage");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainScript",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Output",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserScript",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
