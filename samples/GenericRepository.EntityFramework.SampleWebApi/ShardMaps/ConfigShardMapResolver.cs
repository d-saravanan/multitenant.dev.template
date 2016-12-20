using MultiTenantRepository.Shards.Contracts;
using System;
using System.Configuration;
using System.Data.Common;

namespace MultiTenantRepositry.EF.Api.ShardMaps
{
    public class ConfigShardMapResolver : ITenantShardResolver
    {
        public DbConnection GetConnection(Guid tenantId, string connectionStringName)
        {
            return new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);
        }
    }
}