namespace MultiTenantRepository.EntityFramework
{
    /// <summary>
    /// Entity Framework interface implementation for IRepository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity which implements IEntity of int</typeparam>
    public interface IMultiTenantEFRepository<TEntity> : IMultiTenantEFRepository<TEntity, int>
        where TEntity : class, IMultiTenantEntity<int>
    {

    }
}