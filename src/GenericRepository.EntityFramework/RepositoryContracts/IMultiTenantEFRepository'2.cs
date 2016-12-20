using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MultiTenantRepository.EntityFramework
{
    /// <summary>
    /// Entity Framework interface implementation for IRepository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TId">Type of entity Id</typeparam>
    public interface IMultiTenantEFRepository<TEntity, TId> : IMultiTenantRepository<TEntity, TId>
        where TEntity : class, IMultiTenantEntity<TId>
        where TId : IComparable
    {
        /// <summary>
        /// Gets all the entities including the ones based on the supplied include properties
        /// </summary>
        /// <param name="includeProperties">The properties to include while fetching the entity data</param>
        /// <param name="tenantId">The Tenant Identifier</param>
        /// <returns>The collection of querable entities</returns>
        Task<IQueryable<TEntity>> GetAllIncludingAsync(Guid tenantId, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the single entity including the ones based on the supplied include properties
        /// </summary>
        /// <param name="includeProperties">The properties to include while fetching the entity data</param>
        /// <param name="id">The identifier </param>
        /// <param name="tenantId">The tenant identifier</param>
        /// <returns>The single entity</returns>
        Task<TEntity> GetSingleIncludingAsync(TId id, Guid tenantId, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="tenantId">The tenant identifier</param>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateAsync<TKey>(Guid tenantId, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="tenantId">The tenant identifier</param>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <param name="includeProperties">The properties to include</param>
        /// <param name="predicate">The predicate function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateAsync<TKey>(Guid tenantId, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters in descending order
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="tenantId">The tenant identifier</param>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateDescendingAsync<TKey>(Guid tenantId, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Gets the paginated result for the entity based on the supplied paramters in descending order
        /// </summary>
        /// <typeparam name="TKey">The type of the primary key</typeparam>
        /// <param name="tenantId">The tenant identifier</param>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="keySelector">The key selector function</param>
        /// <param name="includeProperties">The properties to include</param>
        /// <param name="predicate">The predicate function</param>
        /// <returns>Paginaged resultset of the entity data</returns>
        Task<PaginatedList<TEntity>> PaginateDescendingAsync<TKey>(Guid tenantId, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}