using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Services
{
    public class RepoTest1 : BaseRepo
    {
        public DbSet<Test1> Tests1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(@"Test1");
        }

        public override string TargetDbName => "lol";
        public override string TargetSchemaName => "dbo";
        public override string TargetTableName => "table1";
    }
}
