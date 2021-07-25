using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Services
{
    public class BaseDbContext : DbContext
    {
        /* Generic Repo */

        // TODO find duplicates
        public List<T> ItemsGetAll<T>() where T : BaseModel => Set<T>().OrderBy(x=>x.Id).ToList();

        public int ItemsGetCount<T>() where T : BaseModel => ItemsGetAll<T>().Count;

        public T ItemGetById<T>(int id) where T : BaseModel => 
            ItemsGetAll<T>().FirstOrDefault(x=>x.Id == id);
        
        public void ItemAddList<T>(List<T> list) where T : BaseModel
        {
            Set<T>().AddRange(list);
            SaveChanges();
        }
    }
}
