using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rki.ImportToSql.Migrations
{
    public partial class MultipleTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImiraImport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lfdnr = table.Column<int>(type: "int", nullable: false),
                    Tranche = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strasse_HNR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresszusatz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GebDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ortsteil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geburtsland_original = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geburtsland = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geburtsort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Familienstand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat1_original = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat2_original = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat3_original = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staat_agg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sperrvermerk = table.Column<int>(type: "int", nullable: false),
                    Gebdat_gesetzt = table.Column<int>(type: "int", nullable: false),
                    ImportTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImiraImport", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImiraImport");
        }
    }
}
