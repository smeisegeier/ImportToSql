﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rki.ImportToSql.Services;

namespace Rki.ImportToSql.Migrations.InterfaceDb
{
    [DbContext(typeof(InterfaceDbContext))]
    partial class InterfaceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rki.ImportToSql.Models.Domain.GsProzessdaten", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ANR")
                        .HasColumnType("int");

                    b.Property<int?>("Blut")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Einwilligung")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FB3_Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FB3_stat")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FB7_Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FB7_stat")
                        .HasColumnType("int");

                    b.Property<int?>("FarbeId")
                        .HasColumnType("int");

                    b.Property<int?>("GA_NCoV")
                        .HasColumnType("int");

                    b.Property<int?>("Geburtsjahr")
                        .HasColumnType("int");

                    b.Property<int?>("Geburtsmonat")
                        .HasColumnType("int");

                    b.Property<int?>("Geschlecht")
                        .HasColumnType("int");

                    b.Property<DateTime?>("HB1_Datum")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("HB1_Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("HB2_Datum")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("HB2_Status")
                        .HasColumnType("bit");

                    b.Property<int?>("HaushaltId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ImportTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IstAnkerperson")
                        .HasColumnType("bit");

                    b.Property<bool>("IstIndexperson")
                        .HasColumnType("bit");

                    b.Property<int?>("KitaId")
                        .HasColumnType("int");

                    b.Property<int?>("MNA")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ortsteil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PLZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QNA")
                        .HasColumnType("int");

                    b.Property<int?>("Rolle")
                        .HasColumnType("int");

                    b.Property<int?>("Speichel")
                        .HasColumnType("int");

                    b.Property<int?>("Symptomtagebuch")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ImportFromSql", "GsProzessdaten");
                });

            modelBuilder.Entity("Rki.ImportToSql.Models.Domain.Imira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresszusatz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Familienstand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GebDat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gebdat_gesetzt")
                        .HasColumnType("int");

                    b.Property<string>("Geburtsland")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Geburtsland_original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Geburtsort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ImportTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Lfdnr")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ortsteil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PLZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sperrvermerk")
                        .HasColumnType("int");

                    b.Property<string>("Staat1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staat1_original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staat2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staat2_original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staat3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staat3_original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staat_agg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Strasse_HNR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tranche")
                        .HasColumnType("int");

                    b.Property<string>("Vorname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ImportFromSql", "Imira");
                });
#pragma warning restore 612, 618
        }
    }
}
