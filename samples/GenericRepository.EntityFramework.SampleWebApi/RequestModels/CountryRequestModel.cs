using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiTenantRepositry.EF.Api.RequestModels {
    
    public class CountryRequestModel {

        [Required]
        public string Name { get; set; }

        [Required]
        public string ISOCode { get; set; }
    }
}