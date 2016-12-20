using AutoMapper;
using MultiTenantRepositry.EF.Api.Dtos;
using MultiTenantRepositry.EF.Api.Mapping;
using MultiTenantRepositry.EF.Core.Entities;

namespace MultiTenantRepositry.EF.Api.App_Start
{
    public static class MapperConfig
    {
        private static IMapper _mapper;
        public static IMapper RegisterMappings()
        {
            AutoMapperConfig.Configure();
            AutoMapperConfig.InitProfiles<Country, CountryDto>();
            _mapper = new Mapper(Mapper.Configuration);
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return _mapper;
        }

    }
}