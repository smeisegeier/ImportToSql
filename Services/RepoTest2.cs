using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;

namespace Rki.ImportToSql.Services
{
    // Add-Migration Init -Context RepoTest2
    // Update-Database -Context RepoTest2
    public class RepoTest2 : BaseRepo
    {
        // Add-Migration Init -Context RepoImira
        // Update-Database -Context RepoImira
        public static RepoTest2 SingletonRepo { get; } = new RepoTest2();


        public DbSet<Test2> Tests2 { get; set; }
        public DbSet<Imira> ImiraImport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb; database=Test2;trusted_connection=true;");
        }

        public override string TargetDbName => "Test2";
    }

}
