using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Builds a generic repository for all classes below base. 
/// Steps to append new classes: 
/// 1) Add new domain class w/ properties in /models (note: give a meaningful hash, int Id is preset)
/// 2) This needs a DbContext for the Target Database, if not already present
/// 2a) when using db-first approach: packetmanager:
/// Scaffold-DbContext "Server=abt2sqldev01;Database=InterfaceDb;Trusted_Connection=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Temp -UseDatabaseNames
/// 2b) when using code-first (have DbSet and override OnConfiguration() w/ connString, also give auxiliary properties) 
/// Add-Migration Schema03 -Context RepoSchema03
/// Update-Database -Context RepoImira
/// 3a) Add Repo for target Db under /services
/// 3b) Tweak connection strings, paths etc. to fit to each other
/// 4) Add a csvhelper mapping class for domain classes, including validation
/// 5) Register a new FileSchema, using all these classes
/// 6) complete selector in viewModel (onUpload)
/// </summary>
namespace Rki.ImportToSql.Services
{
    public abstract class BaseRepo : DbContext
    {
        public abstract string TargetDbName { get; }

        /// <summary>
        /// Determines Network area of connection
        /// </summary>
        public abstract ApplicationNetworkModeType _ApplicationNetworkModeType {get;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        /// <summary>
        /// Default ctor for DbContexts
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }


        /// <summary>
        /// Checks if the list first element hash value exists in table
        /// </summary>
        /// <typeparam name="T">type of objects</typeparam>
        /// <param name="list">list of all objects</param>
        /// <returns>true | false</returns>
        public bool ItemsExist<T>(IList<T> list)  where T : BaseModel => ItemsGetAll<T>().Any(x => x.Hash == list.First().Hash);

        public List<T> ItemsGetAll<T>() where T : BaseModel => Set<T>().OrderBy(x=>x.Id).ToList();

        public int ItemsGetCount<T>() where T : BaseModel => ItemsGetAll<T>().Count;

        public T ItemGetById<T>(int id) where T : BaseModel => 
            ItemsGetAll<T>().FirstOrDefault(x=>x.Id == id);

        public T ItemGetByHash<T>(T obj) where T : BaseModel => ItemsGetAll<T>().FirstOrDefault(x => x.Hash == obj.Hash);

        public int ItemAddList<T>(IEnumerable<T> list) where T : BaseModel
        {
            Set<T>().AddRange(list);
            return SaveChanges();
        }

        public int ItemDelete<T>(T obj) where T : BaseModel
        {
            if (obj is not null)
                Set<T>().Remove(obj);
            return SaveChanges();
        }

        public int ItemDeleteDuplicates<T>(IEnumerable<T> list) where T : BaseModel
        {
            int count = 0;
            // delete duplicates
            list.ToList().ForEach(x =>
            {
                count += ItemDelete(ItemGetByHash(x));
            });
            return count;
        }
    }
}
