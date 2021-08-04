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
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
