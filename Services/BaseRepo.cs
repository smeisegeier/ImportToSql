﻿using Microsoft.EntityFrameworkCore;
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
    /// 2) This needs a DbContext for the Target Database, if not already present
    /// 2a) when using db-first approach: packetmanager:
    /// Scaffold-DbContext "Server=abt2sqldev01;Database=InterfaceDb;Trusted_Connection=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Temp -UseDatabaseNames
    /// 2b) when using code-first: packetmanager: 
    /// Add-Migrations Schema03 -Context RepoSchema03
    /// 3a) Add Repo to that class under /services
    /// 3b) In that repo, have DbSet and override OnConfiguration() w/ connString, also give auxiliary properties. 
    /// 3c) Tweak connection strings, paths etc. to fit to each other
    /// 4) Add a dto class that only has the transfered columns (no int columns!)
    /// 4a) write a mapper dto -> domain (static implicit operator)
    /// 5) Register a new FileSchema, using all these classes
    /// 6) complete selector in viewModel (onUpload)
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
