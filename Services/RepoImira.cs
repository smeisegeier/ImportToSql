using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Services
{
    public class RepoImira : BaseRepo
    {
        // Add-Migration Init -Context RepoImira
        // Update-Database -Context RepoImira
        public DbSet<Imira> ImiraImport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(@"Imira");
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb; database=Imira;trusted_connection=true;TrustServerCertificate=true;");
        }

        public override string TargetDbName => "Imira";
        public override string TargetSchemaName => "dbo";
        public override string TargetTableName => "ImiraImport";
    }
}
