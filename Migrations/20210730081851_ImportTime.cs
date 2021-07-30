using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rki.ImportToSql.Migrations
{
    public partial class ImportTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ImportTime",
                table: "Tests2",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportTime",
                table: "Tests2");
        }
    }
}
