using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Services
{
    public class RepoSchema03 : BaseRepo
    {
        public DbSet<Schema03Anmeldungen> Anmeldungen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb; database=DeaClearing;trusted_connection=true;");
        }

        public override string TargetPathInfo => "[server].[DeaClearing].[dbo].[Anmeldungen]";
    }
}
