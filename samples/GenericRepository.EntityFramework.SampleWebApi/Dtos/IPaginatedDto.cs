using System.Collections.Generic;

namespace MultiTenantRepositry.EF.Api.Dtos {

    public interface IPaginatedDto<out TDto> where TDto : IDto {

        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int TotalPageCount { get; set; }

        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }

        IEnumerable<TDto> Items { get; }
    }
}