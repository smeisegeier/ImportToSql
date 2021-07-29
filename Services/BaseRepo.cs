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
    /// <summary>
    /// Builds a generic repository for all classes below base. 
    /// Steps to append new classes: 
    /// 1) Add new domain class w/ properties in /models (note: give a meaningful hash, int Id is preset)
    /// 2a) Add a Repo to that class under /services
    /// 2b) In that repo, have DbSet and override OnConfiguration() w/ connString, also give auxiliary properties. 
    /// 2c) Tweak connection strings, paths etc. to fit to each other
    /// 2d) (?) Register repo in packetmanager: Add-Migrations Schema03 -Context RepoSchema03
    /// 3) Add a dto class that only has the transfered columns
    /// 4) Register a new FileSchema, using these classes
    /// 5) complete selector in viewModel (onUpload)
    /// </summary>
    public abstract class BaseRepo : DbContext
    {

        public abstract string TargetPathInfo { get; }


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
        
        public int ItemAddList<T>(IEnumerable<T> list) where T : BaseModel
        {
            Set<T>().AddRange(list);
            return SaveChanges();
        }
    }
}
