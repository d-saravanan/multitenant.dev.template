using AutoMapper;
using MultiTenancy.Core.ProviderContracts;
using MultiTenantRepository;
using MultiTenantRepositry.EF.Api.Dtos;
using System;
using System.Web.Http;

namespace MultiTenantRepositry.EF.Api.Controllers
{
    //TODO: Add more here to offload normal entity controllers...
    /// <summary>
    /// The BaseApi Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BaseApiController : ApiController
    {
        internal readonly IMapper _mapper;
        internal readonly IUserContextDataProvider _userContext;

        public Guid TenantId { get { return _userContext.TenantId; } }

        public BaseApiController(IMapper mapper, IUserContextDataProvider userContext)
        {
            _mapper = mapper;
            _userContext = userContext;
        }

        internal Uri GetCountryLink<TId>(TId id)
        {
            return new Uri(Request.RequestUri + "/" + id);
        }

        internal TDto DtoResult<TEntity, TDto>(TEntity entity)
        {
            return entity.ToDto<TEntity, TDto>(_mapper);
        }
        internal PaginatedDto<TDto> DtoResult<TEntity, TDto>(PaginatedList<TEntity> paginatedEntity)
            where TDto : IDto
        {
            return paginatedEntity.ToDto<TEntity, TDto>(_mapper);
        }
    }

    public static class DtoExtensions
    {
        public static TDto ToDto<TEntity, TDto>(this TEntity entity, IMapper mapper)
        {
            return mapper.Map<TEntity, TDto>(entity);
        }

        public static PaginatedDto<TDto> ToDto<TEntity, TDto>(this PaginatedList<TEntity> paginatedEntity, IMapper mapper)
            where TDto : IDto
        {
            return mapper.Map<PaginatedList<TEntity>, PaginatedDto<TDto>>(paginatedEntity);
        }
    }
}