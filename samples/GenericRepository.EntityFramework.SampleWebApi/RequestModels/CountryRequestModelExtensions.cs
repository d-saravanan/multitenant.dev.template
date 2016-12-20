using MultiTenantRepositry.EF.Core.Entities;

namespace MultiTenantRepositry.EF.Api.RequestModels
{

    public static class CountryRequestModelExtensions {

        public static Country ToCountry(this CountryRequestModel requestModel, Country source) {

            source.Name = requestModel.Name;
            source.ISOCode = requestModel.ISOCode;

            return source;
        }
    }
}