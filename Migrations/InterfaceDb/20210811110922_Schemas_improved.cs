using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rki.ImportToSql.Migrations.InterfaceDb
{
    public partial class Schemas_improved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GsProzessdaten");

            migrationBuilder.EnsureSchema(
                name: "Imira");

            migrationBuilder.CreateTable(
                name: "ImportFromSql",
                schema: "GsProzessdaten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANR = table.Column<int>(type: "int", nullable: false),
                    KitaId = table.Column<int>(type: "int", nullable: true),
                    HaushaltId = table.Column<int>(type: "int", nullable: true),
                    Geburtsmonat = table.Column<int>(type: "int", nullable: true),
                    Geburtsjahr = table.Column<int>(type: "int", nullable: true),
                    Geschlecht = table.Column<int>(type: "int", nullable: true),
                    FB3_stat = table.Column<int>(type: "int", nullable: true),
                    FB7_stat = table.Column<int>(type: "int", nullable: true),
                    Einwilligung = table.Column<int>(type: "int", nullable: true),
                    MNA = table.Column<int>(type: "int", nullable: true),
                    Speichel = table.Column<int>(type: "int", nullable: true),
                    Blut = table.Column<int>(type: "int", nullable: true),
                    GA_NCoV = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HB1_Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HB2_Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HB1_Status = table.Column<bool>(type: "bit", nullable: true),
                    HB2_Status = table.Column<bool>(type: "bit", nullable: true),
                    Ort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ortsteil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarbeId = table.Column<int>(type: "int", nullable: true),
                    Rolle = table.Column<int>(type: "int", nullable: true),
                    FB3_Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FB7_Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IstIndexperson = table.Column<bool>(type: "bit", nullable: false),
                    IstAnkerperson = table.Column<bool>(type: "bit", nullable: false),
                    Symptomtagebuch = table.Column<int>(type: "int", nullable: true),
                    QNA = table.Column<int>(type: "int", nullable: true),
                    ImportTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportFromSql", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportFromSql",
                schema: "Imira",
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
                    table.PrimaryKey("PK_ImportFromSql", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportFromSql",
                schema: "GsProzessdaten");

            migrationBuilder.DropTable(
                name: "ImportFromSql",
                schema: "Imira");
        }
    }
}
