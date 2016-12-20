namespace MultiTenantRepository.EntityFramework
{
    /// <summary>
    /// IEntityRepository implementation for DbContext instance where the TId type is Int32.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public class MultiTenantRepository<TEntity> : MultiTenantRepository<TEntity, int>, IMultiTenantEFRepository<TEntity>
        where TEntity : class, IMultiTenantEntity<int>
    {
        /// <summary>
        /// Constructs an entity repository based on the supplied context
        /// </summary>
        /// <param name="dbContext">The db context</param>
        public MultiTenantRepository(IEntitiesContext dbContext)
            : base(dbContext)
        {

        }
    }
}