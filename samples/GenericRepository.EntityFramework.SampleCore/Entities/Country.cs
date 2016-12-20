using MultiTenantRepository;
using System;
using System.Collections.Generic;

namespace MultiTenantRepositry.EF.Core.Entities
{

    // NOTE: IEntity provides the integer Id parameter.
    // If your id type is other than integer, use IEntity<TId>
    public class Country : IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public virtual ICollection<Resort> Resorts { get; set; }
    }
}