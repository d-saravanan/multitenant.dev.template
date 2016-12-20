using AutoMapper;
using MultiTenantRepository;
using MultiTenantRepositry.EF.Api.Dtos;
using MultiTenantRepositry.EF.Api.RequestModels;
using MultiTenantRepositry.EF.Core.Entities;
using System;
using System.Linq;

namespace MultiTenantRepositry.EF.Api.Mapping
{
    /// <summary>
    /// The automapper configurator
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Gets the initialized mapperConfiguration
        /// </summary>
        public static MapperConfiguration MapperConfiguration;

        /// <summary>
        /// Configures the automapper
        /// </summary>
        public static void Configure()
        {
            RequestModelToEntityMapping();
        }

        private static void RequestModelToEntityMapping()
        {
            //MapperConfiguration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<CountryRequestModel, Country>();
            //});
        }

        /// <summary>
        /// Initialzes the profiles 
        /// </summary>
        /// <typeparam name="TEntity">The type of the business entity</typeparam>
        /// <typeparam name="TDto">the type of the data transfer object</typeparam>
        public static void InitProfiles<TEntity, TDto>()
            where TDto : IDto
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new PaingatedEntityProfile<TEntity, TDto>()));
        }
    }

    /// <summary>
    /// The paged entity profile
    /// </summary>
    /// <typeparam name="TEntity">The type of the business entity</typeparam>
    /// <typeparam name="TDto">The type of the data transfer object</typeparam>
    public class PaingatedEntityProfile<TEntity, TDto> : Profile
        where TDto : IDto
    {
        /// <summary>
        /// The Configuration of the entity profile
        /// </summary>
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<CountryRequestModel, Country>()
                .ForMember(dest => dest.TenantId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Resorts, opt => opt.Ignore());
            CreateMap<TEntity, TDto>();
            CreateMap<PaginatedList<TEntity>, PaginatedDto<TDto>>()
                            .ForMember(dest => dest.Items,
                                       opt => opt.MapFrom(
                                           src => src.Select(
                                               entity => Mapper.Map<TEntity, TDto>(entity))));
        }
    }

}