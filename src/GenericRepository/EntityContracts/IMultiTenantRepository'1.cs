
namespace MultiTenantRepository {
    
    /// <summary>
    /// The Repository contract
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IMultiTenantRepository<TEntity> : IMultiTenantRepository<TEntity, int>
        where TEntity : class, IMultiTenantEntity<int> {
    }
}