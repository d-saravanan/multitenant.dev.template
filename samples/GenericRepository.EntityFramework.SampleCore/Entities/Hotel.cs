using MultiTenantRepository;
using System;

namespace MultiTenantRepositry.EF.Core.Entities
{

    // NOTE: IEntity provides the integer Id parameter.
    // If your id type is other than integer, use IEntity<TId>
    public class Hotel : IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public int ResortId { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public Resort Resort { get; set; }
    }
}
