using MultiTenantRepositry.EF.Core.Entities;
using MultiTenantRepository;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantRepositry.EF.Core
{
    public static class HotelRepositoryExtensions
    {
        public static Task<IQueryable<Hotel>> GetAllByResortId(this IMultiTenantRepository<Hotel, int> hotelRepository,
            System.Guid tenantId, int resortId)
        {
            return hotelRepository.FindByAsync(tenantId, x => x.ResortId == resortId);
        }
    }
}