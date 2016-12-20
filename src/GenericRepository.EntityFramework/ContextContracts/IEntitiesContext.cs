using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MultiTenantRepository.EntityFramework
{
    /// <summary>
    /// The Contract for the entities context
    /// </summary>
    public interface IEntitiesContext : IDisposable
    {
        /// <summary>
        /// Sets the entity in the db set
        /// </summary>
        /// <typeparam name="TEntity">The entity to be set</typeparam>
        /// <returns>The dbset</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Sets the entity state as added
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="entity">the entity</param>
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Sets the entity state as modified
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="entity">the entity</param>
        void SetAsModified<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Sets the entity state as deleted
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="entity">the entity</param>
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Saves the state of the entity to the db
        /// </summary>
        /// <returns>The number of records affected by the save operation</returns>
        Task<int> SaveChangesAsync();
    }
}