using Microsoft.EntityFrameworkCore;
using Rki.ImportToSql.Models;
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
    /// 1) Register new class w/ properties in /models
    /// 2) Assign a Repo to that class under /services
    /// 3) In that repo, have DbSet and override OnConfiguration() w/ connString, also give auxiliary properties. 
    /// 4) This repo must be placed as a *static* property in the class for easy access, incl. initializer. 
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
        public bool ItemsExist<T>(List<T> list) where T : BaseModel => ItemsGetAll<T>().Any(x => x.Hash == list.First().Hash);

        public List<T> ItemsGetAll<T>() where T : BaseModel => Set<T>().OrderBy(x=>x.Id).ToList();

        public int ItemsGetCount<T>() where T : BaseModel => ItemsGetAll<T>().Count;

        public T ItemGetById<T>(int id) where T : BaseModel => 
            ItemsGetAll<T>().FirstOrDefault(x=>x.Id == id);
        
        public int ItemAddList<T>(List<T> list) where T : BaseModel
        {
            Set<T>().AddRange(list);
            return SaveChanges();
        }
    }
}
