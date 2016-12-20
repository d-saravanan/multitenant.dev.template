using System;
using System.Linq;
using System.Collections.Generic;

namespace MultiTenantRepository
{
    /// <summary>
    /// List object to represent the paginated collection.
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// The page index
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// The page size
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// The total count
        /// </summary>
        public int TotalCount { get; private set; }
        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPageCount { get; private set; }

        /// <summary>
        /// Whether there is a previous page 
        /// </summary>
        public bool HasPreviousPage
        {

            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Whether there is a next page
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPageCount);
            }
        }

        /// <summary>
        /// Constructs an instance of the paginated list form the source
        /// </summary>
        /// <param name="source">source collection</param>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="totalCount">The total count</param>
        public PaginatedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            if (source == null || source.Count() < 1)
                throw new ArgumentNullException("source cannot be null or empty");

            if (pageIndex < 0 || pageSize < 0 || totalCount < 0) return;

            AddRange(source);
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}