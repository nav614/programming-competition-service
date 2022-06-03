using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammingCompetitionService.Migrations
{
    public partial class CompletedTaskCreateDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "CompletedTasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "CompletedTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "CompletedTasks");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "CompletedTasks",
                type: "BLOB",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
