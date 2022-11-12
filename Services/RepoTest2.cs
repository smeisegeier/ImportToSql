using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;

namespace Rki.ImportToSql.Services
{

    public class RepoTest2 : BaseRepo
    {
        // Add-Migration Init -Context RepoImira
        // Update-Database -Context RepoImira
        public static RepoTest2 SingletonRepo { get; } = new RepoTest2();


        public DbSet<Test2> Tests2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb; database=Test2;trusted_connection=true;");
        }

        public override string TargetDbName => "Test2";
        public override ApplicationNetworkModeType _ApplicationNetworkModeType => ApplicationNetworkModeType.LOCAL;
    }

}
