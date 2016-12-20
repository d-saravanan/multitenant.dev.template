using System;
namespace MultiTenantRepository
{
    /// <summary>
    /// The entity contract
    /// </summary>
    /// <typeparam name="TId">Data Type of the primary key</typeparam>
    public interface IMultiTenantEntity<TId> where TId : IComparable
    {
        /// <summary>
        /// The primary key
        /// </summary>
        TId Id { get; set; }

        /// <summary>
        /// The tenant identifier
        /// </summary>
        Guid TenantId { get; set; }
    }
}