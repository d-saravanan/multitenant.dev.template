using MultiTenantRepository.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MultiTenantRepository
{
    /// <summary>
    /// The Repository contract for the entity with the key type of TId
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TId">the type of the primary key in the entity</typeparam>
    public interface IMultiTenantRepository<TEntity, TId>
        where TEntity : class, IMultiTenantEntity<TId>
        where TId : IComparable
    {
        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <param name="tenantId">The tenant identifier</param>
        /// <returns>IQueryable collection of the entities</returns>
        IQueryable<TEntity> GetAll(Guid tenantId);

        /// <summary>
        /// Finds the entities that match the given predicate
        /// </summary>
        /// <param name="tenantId">The tenant identifier</param>
        /// <param name="predicate">The predicate</param>
        /// <returns>An IQueryable collection of matched entities</returns>
        Task<IQueryable<TEntity>> FindByAsync(Guid tenantId, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets the paginated list of entitybased on the page index and the number of records per page
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="tenantId">The Tenant identifier</param>
        /// <returns>An IQueryable collection of matched entities</returns>
        Task<PaginatedList<TEntity>> PaginateAsync(Guid tenantId, int pageIndex, int pageSize);

        /// <summary>
        /// Gets a single entity based on the supplied id
        /// </summary>
        /// <param name="id">The unique identifier for the entity record</param>
        /// <returns>The entity</returns>
        Task<TEntity> GetSingleAsync(TId id);

        /// <summary>
        /// Gets the entities that match the given set of ids
        /// </summary>
        /// <param name="ids">The collection of identifiers</param>
        /// <param name="tenantId">The Tenant Identifier</param>
        /// <returns>A collection of entities</returns>
        Task<System.Collections.Generic.IEnumerable<TEntity>> GetByIdsAsync(Guid tenantId, TId[] ids);

        /// <summary>
        /// Performs the search operation based on the specified search condition and returns the collection of data back to the caller
        /// </summary>
        /// <param name="searchCondition">The implementation of the base search conditon</param>
        /// <returns>Collection of tenant entity data</returns>
        Task<System.Collections.Generic.IEnumerable<TEntity>> SearchAsync(BaseSearchCondition<TId> searchCondition);

        /// <summary>
        /// Asynchronously adds an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Adds a graph of related entities
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task AddGraphAsync(TEntity entity);

        /// <summary>
        /// Asynchronously edits an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task EditAsync(TEntity entity);

        /// <summary>
        /// Asynchronously delete an entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>a future to be handled by the caller</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously save an entity
        /// </summary>
        /// <returns>The number of records affected by the save operation</returns>
        Task<int> SaveAsync();
    }
}