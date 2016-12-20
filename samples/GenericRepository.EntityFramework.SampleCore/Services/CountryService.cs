using MultiTenantRepositry.EF.Core.Entities;
using MultiTenantRepository;
using MultiTenantRepository.Entities;
using MultiTenantRepository.Enums;
using MultiTenantServices.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantRepositry.EF.Core.Services
{
    public class CountryService : MultiTenantServices<Country, int>
    {
        IMultiTenantRepository<Country, int> _repository = null;

        public CountryService(IMultiTenantRepository<Country, int> repository) : base(repository)
        {
            _repository = repository;
        }

        protected override void CompleteServiceCall(EntityOperations operation, params Country[] entity)
        {
            Trace.WriteLine("completed the operation" + operation);
        }

        protected override void OperationFailed(EntityOperations operation, params Country[] entity)
        {
            Trace.WriteLine(operation + " failed for the country entity");
        }

        public override async Task<PaginatedList<Country>> SearchAsync(BaseSearchCondition<int> searchCondition)
        {
            if (searchCondition == null) return null;

            var result = await _repository.PaginateAsync(searchCondition.TenantId, searchCondition.PageNo, searchCondition.RecordsPerPage);
            return result;
        }

        protected override bool TryPostProcessEntity(EntityOperations operation, out string message, params Country[] entity)
        {
            message = string.Empty;
            return true;
        }

        protected override bool TryPreProcessEntity(Country entity, EntityOperations operation, out string message)
        {
            message = string.Empty;
            return true;
        }

        protected override void UnPrivilegedAccess(Country entity)
        {
            return;
        }

        protected override void UnPrivilegedAccess(IEnumerable<int> entityIds)
        {
            return;
        }

        protected override bool ValidateEntity(Country entity, EntityOperations operation, out IEnumerable<string> messages)
        {
            messages = Enumerable.Empty<string>();
            return true;
        }

        protected override bool ValidateEntityIds(EntityOperations operation, out Dictionary<string, string> messages, params int[] entityIds)
        {
            messages = new Dictionary<string, string>();
            return true;
        }
    }
}
