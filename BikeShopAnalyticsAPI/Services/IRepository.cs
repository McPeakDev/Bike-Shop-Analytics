///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  ProMan/ProMan
//	File Name:         IRepository.cs
//	Course:            CSCI 3110-001 - Advanced Web Dev & Design
//	Author:            Matthew McPeak, McPeakML@etsu.edu, East Tennessee State University
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BikeShopAnalyticsAPI.Services
{
    /// <summary>
    /// An Abstract Repository for ProjectDbContext
    /// </summary>
    /// <typeparam name="TEntity">Object Type</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Reads a TEntity from the Db, with a comparing Function and any sub-entities to include. 
        /// </summary>
        /// <param name="predicate">A Comparing Function in the form of a lambda</param>
        /// <param name="includes">An array of lambda expressions for sub-entities to be included</param>
        /// <returns>A TEntity</returns>
        TEntity Read(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Reads The Entire table of TEntity, with any sub-entities to include. 
        /// </summary>
        /// <param name="includes">An array of lambda expressions for sub-entities to be included</param>
        /// <returns>An IQueryable of TEntity</returns>
        IQueryable<TEntity> ReadAll(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Creates a TEntity in the table of Type of TEntity
        /// </summary>
        /// <param name="obj">A object of TEntity</param>
        /// <returns>A TEntity</returns>
        TEntity Create(TEntity obj);

        /// <summary>
        /// Updates a TEntity in the table of Type of TEntity
        /// </summary>
        /// <param name="obj">A object of TEntity</param>
        void Update(TEntity obj);

        /// <summary>
        /// Deletes a TEntity in the table of Type of TEntity
        /// </summary>
        /// <param name="obj">A object of TEntity</param>
        void Delete(TEntity obj);
    }
}
