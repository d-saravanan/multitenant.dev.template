using System;
using System.Data.Common;

namespace MultiTenantRepository.Shards.Contracts
{
    /// <summary>
    /// The tenant specific shard resolver to get the shard for the storage of tenant specific data
    /// </summary>
    public interface ITenantShardResolver
    {
        /// <summary>
        /// Gets the tenant specific shard based connection from the metadata
        /// </summary>
        /// <param name="tenantId">The tenant identifier</param>
        /// <param name="connectionStringName">The Connection string name</param>
        /// <returns>
        /// The DbConnection for the specific shard per tenant
        /// <see cref="DbConnection"/> 
        /// </returns>
        DbConnection GetConnection(Guid tenantId, string connectionStringName);
    }
}
