using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;

#nullable disable

namespace Rki.ImportToSql.Services
{
    public partial class InterfaceDbContext : BaseRepo
    {
        public override string TargetPathInfo => "[abt2sqldev01].[InterfaceDb].[COALA].[COALA_GS_Prozessdaten]";

        public virtual DbSet<GsProzessdaten> GsProzessdatens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=abt2sqldev01;Database=InterfaceDb;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<GsProzessdaten>(entity =>
            {
                entity.ToTable("GS_Prozessdaten", "COALA");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Anr).HasColumnName("ANR");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Fb3Datum)
                    .HasColumnType("datetime")
                    .HasColumnName("FB3_Datum");

                entity.Property(e => e.Fb3Stat).HasColumnName("FB3_stat");

                entity.Property(e => e.Fb7Datum)
                    .HasColumnType("datetime")
                    .HasColumnName("FB7_Datum");

                entity.Property(e => e.Fb7Stat).HasColumnName("FB7_stat");

                entity.Property(e => e.GaNcoV).HasColumnName("GA_NCoV");

                entity.Property(e => e.Hb1Datum)
                    .HasColumnType("datetime")
                    .HasColumnName("HB1_Datum");

                entity.Property(e => e.Hb1Status).HasColumnName("HB1_Status");

                entity.Property(e => e.Hb2Datum)
                    .HasColumnType("datetime")
                    .HasColumnName("HB2_Datum");

                entity.Property(e => e.Hb2Status).HasColumnName("HB2_Status");

                entity.Property(e => e.Mna).HasColumnName("MNA");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Plz).HasColumnName("PLZ");

                entity.Property(e => e.Qna).HasColumnName("QNA");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
