using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;

#nullable disable

namespace Rki.ImportToSql.Services
{
    // Add-Migration XXX -Context InterfaceDbContext
    // Update-Database -Context InterfaceDbContext
    public class InterfaceDbContext : BaseRepo
    {
        public static InterfaceDbContext SingletonRepo { get; } = new InterfaceDbContext();
        public override string TargetDbName => "InterfaceDb";


        public virtual DbSet<GsProzessdaten> GsProzessdatens { get; set; }
        public DbSet<Imira> ImiraImport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=abt2sqldev01;Database=InterfaceDb;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // default settings
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Imira>().ToTable(Globals.DEFAULT_TABLE_NAME, nameof(Imira));
            modelBuilder.Entity<GsProzessdaten>().ToTable(Globals.DEFAULT_TABLE_NAME, nameof(GsProzessdaten));
        }

        public override ApplicationNetworkModeType _ApplicationNetworkModeType => ApplicationNetworkModeType.LAN;
    }
}
