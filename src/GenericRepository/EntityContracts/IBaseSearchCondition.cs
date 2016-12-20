using System;
using System.Collections.Generic;

namespace MultiTenantRepository.EntityContracts
{
    /// <summary>
    /// The IEntity Search condition,used to search for entity based on entity properties
    /// </summary>
    /// <typeparam name="TId">The type of the uniqueidentifier used to uniquely identify the entity record</typeparam>
    public interface IBaseSearchCondition<TId> where TId : IComparable
    {
        /// <summary>
        /// The identifier
        /// </summary>
        TId Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier
        /// </summary>
        Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the collection of tenant identifiers
        /// </summary>
        IEnumerable<Guid> TenantIds { get; set; }

        /// <summary>
        /// The collection of identifiers
        /// </summary>
        IEnumerable<TId> Ids { get; set; }

        /// <summary>
        /// Indicates the status of the records
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The page number of the current page, used when server side paging
        /// </summary>
        int PageNo { get; set; }

        /// <summary>
        /// The number of records to be returned per paged data window
        /// </summary>
        int RecordsPerPage { get; set; }
    }
}