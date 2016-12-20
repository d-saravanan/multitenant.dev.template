﻿using MultiTenantRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiTenantRepositry.EF.Core.Entities
{

    // NOTE: IEntity provides the integer Id parameter.
    // If your id type is other than integer, use IEntity<TId>
    public class Resort : IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public Country Country { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}