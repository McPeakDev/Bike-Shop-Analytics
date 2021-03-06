﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  ProMan/ProMan
//	File Name:         ProjectDbRepository.cs
//	Course:            CSCI 3110-001 - Advanced Web Dev & Design
//	Author:            Matthew McPeak, McPeakML@etsu.edu, East Tennessee State University
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using BikeShopAnalyticsAPI.Models;
using System.Threading.Tasks;

namespace BikeShopAnalyticsAPI.Services
{
    /// <summary>
    /// A Class to Query the ProjectDb with the Type of TEntity. Implements IRepository of TEntity where TEntity is a class 
    /// </summary>
    /// <typeparam name="TEntity">Object Type</typeparam>
    public class DbRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Project DB Context
        /// </summary>
        private BikeShopContext _db;

        /// <summary>
        /// DBSet of Type TEntity
        /// </summary>
        private DbSet<TEntity> _table;

        /// <summary>
        /// Default Constructor. Assigns private variables.
        /// </summary>
        /// <param name="db"></param>
        public DbRepository(BikeShopContext db)
        {
            //Assign _db to db;
            _db = db;

            //Assign _table to _db.Set of TEntity
            _table = _db.Set<TEntity>();
        }


        /// <summary>
        /// Creates a TEntity in the table of Type of TEntity
        /// </summary>
        /// <param name="obj">A object of TEntity</param>
        /// <returns>A TEntity</returns>
        public async Task<TEntity> Create(TEntity obj)
        {
            //Add the obj to the table, save the object, and return the object. 
            await _table.AddAsync(obj);
            await Save();
            return obj;
        }

        /// <summary>
        /// Reads a TEntity from the Db, with a comparing Function and any sub-entities to include. 
        /// </summary>
        /// <param name="predicate">A Comparing Function in the form of a lambda</param>
        /// <param name="includes">An array of lambda expressions for sub-entities to be included</param>
        /// <returns>A TEntity</returns>
        public Task<TEntity> Read(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            //Return TEntity Where Predicate is True and include all IncludeProperties from includes
            return Task.Run( () => includes.Aggregate(_table.AsQueryable(), (current, includeProperty) => current.Include(includeProperty)).FirstOrDefault(predicate));

        }

        /// <summary>
        /// Reads The Entire table of TEntity, with any sub-entities to include. 
        /// </summary>
        /// <param name="includes">An array of lambda expressions for sub-entities to be included</param>
        /// <returns>An IQueryable of TEntity</returns>
        public Task<IQueryable<TEntity>> ReadAll(params Expression<Func<TEntity, object>>[] includes)
        {
            //Return IQueryable of TEntity and include all IncludeProperties from includes
            return Task.Run( () => includes.Aggregate(_table.AsQueryable(), (current, includeProperty) => current.Include(includeProperty)));
        }

        /// <summary>
        /// Updates a TEntity in the table of Type of TEntity
        /// </summary>
        /// <param name="obj">A object of TEntity</param>
        public Task Update(TEntity obj)
        {
            //Attach obj to defined table, Tell the entry that it has been modified, Save the Database.
            return Task.Run
                ( async () => 
                    {
                        _table.Attach(obj);
                        _db.Entry(obj).State = EntityState.Modified;
                        await Save();
                    }
                );
        }

        /// <summary>
        /// Deletes a TEntity in the table of Type of TEntity
        /// </summary>
        /// <param name="obj">A object of TEntity</param>
        public Task Delete(TEntity obj)
        {
            //Remove obj from defined table. Save the Database.
            return Task.Run
            ( async () => 
                {
                    _table.Remove(obj);
                    await Save();
                }
            );

        }

        /// <summary>
        /// Saves/Updates the Database.
        /// </summary>
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
