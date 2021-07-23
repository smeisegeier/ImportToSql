using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;

namespace Rki.ImportToSql.Services
{
    public class RepoTest2 : DbContext
    {
        public DbSet<Test2> Tests2 { get; set; }
    }

}
