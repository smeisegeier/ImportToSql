using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;

namespace Rki.ImportToSql.Services
{
    // Add-Migration Init -Context RepoTest2
    // Update-Database -Context RepoTest2
    public class RepoTest2 : BaseRepo
    {
        public DbSet<Test2> Tests2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb; database=Test2;trusted_connection=true;");
        }

        public override string TargetPathInfo => "[server].[db].[schema].[table2]";
    }

}
