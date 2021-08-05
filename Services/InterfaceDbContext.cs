using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;

#nullable disable

namespace Rki.ImportToSql.Services
{
    public class InterfaceDbContext : BaseRepo
    {
        public static InterfaceDbContext SingletonRepo { get; } = new InterfaceDbContext();
        public override string TargetDbName => "InterfaceDb";

        public virtual DbSet<GsProzessdaten> GsProzessdatens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=abt2sqldev01;Database=InterfaceDb;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }
    }
}
