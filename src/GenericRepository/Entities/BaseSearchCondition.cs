namespace MultiTenantRepository.Entities
{
    using EntityContracts;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The entity base entity search condition
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public partial class BaseSearchCondition<TId> : IBaseSearchCondition<TId> where TId : IComparable
    {
        /// <summary>
        /// Gets or sets the tenant identifier
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the collection of tenant identifiers
        /// </summary>
        public IEnumerable<Guid> TenantIds { get; set; }

        /// <summary>
        /// The identifier
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates the status of the records
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The collection of identifiers
        /// </summary>
        public IEnumerable<TId> Ids { get; set; }

        /// <summary>
        /// The page number of the current page, used when server side paging
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// The number of records to be returned per paged data window
        /// </summary>
        public int RecordsPerPage { get; set; }
    }
}