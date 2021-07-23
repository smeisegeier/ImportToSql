using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Services
{
    public class RepoTest1 : DbContext
    {
        public DbSet<Test1> Tests1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(@"Test1");
        }

        public List<Test1> Tests1GetItems() => Tests1.ToList();

        public void Tests1AddItem(List<Test1> list)
        {
            Tests1.AddRange(list);
            SaveChanges();
        }

    }

}
