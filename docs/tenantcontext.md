# The Authentication Mechanism

The following documentation is applicable to the Web Api layer which is the front end in this template. In case of web apps like MVC frontend, the header authentication can be swapped with the cookie authentication, rest are the same.

Currently, there is only `UserId`, `UserName`, `TenantId` as properties in the reference implementation, however as the need grows, there can be more claims added to this and used within the application.

As a best practice, always stop the context provider till the service layer, the DAL layers need not be aware of the context, unless some very context specific request is to be processed .

Consider an example implementation via the CountriesController located here `/master/samples/GenericRepository.EntityFramework.SampleWebApi/Controllers/CountriesController.cs`
Flow of User / Tenant Context

1. User Passes the Authorization Header requesting for the countries data. 
2. The header is parsed by the AuthenticationMiddleware and then the identity is set. This typically happens using a JWT Bearer or Basic mode of authorization. Once the claims are set, they are set in the CurrentPrincipal using ClaimsIdentity. Refer `/samples/GenericRepository.EntityFramework.SampleWebApi/Authorization/CustomAuthentication.cs`
3. The implementation `ClaimsContextDataProvider.cs` reads the claims principal and then set the context values. In places where the User / tenant context is required, just pass the `IUserContextDataProvider` interface as a constructor argument, they will be automatically resolved via the DI.
4. The Countries Controller takes `IUserContextDataProvider` this provider values are inturn consumed within the "BaseApiController". The BaseApiController has the properties for the UserId, TenantId etc. These propeties are getting their values from the userContextDataProvider.
5. In order to get the countries, the controller constructs the searchcondition. The SearchCondition already has a property called as "TenantId" which is obtained from the property mentioned in (4)
6. This search condition is passed on from the controller to the services where it is validated and the same is passed on the repository
7. The enforcement of the "TenantId" is handled automatically via the ```MultiTenantRepository.EntityFramework.MultiTenantRepository<TEntity, TId>```, refer the private method
```private async Task<PaginatedList<TEntity>> PaginateAsync<TKey>(Guid tenantId, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderByType orderByType, params Expression<Func<TEntity, object>>[] includeProperties)```
As described, this method adds the filter clause to the EF Query and then passes the query to the database.

There is no developer intervention / custom EF Repository required as the machinery is already in place to make the filtering happen automatically.

**NOTE**
This template does not use Session as it is a REST Api and also does not persist the TenantContext, its always fetched from the established claims identity.

**Sample Code**
### Repository Level Enforcement of Tenant Security
```
private async Task<PaginatedList<TEntity>> PaginateAsync<TKey>(Guid tenantId, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderByType orderByType, params Expression<Func<TEntity, object>>[] includeProperties)
{
	IQueryable<TEntity> queryable =
		 (orderByType == OrderByType.Ascending)
			? (await GetAllIncludingAsync(tenantId, includeProperties)).OrderBy(keySelector)
			: (await GetAllIncludingAsync(tenantId, includeProperties)).OrderByDescending(keySelector);

	queryable = (predicate != null) ? queryable.Where(predicate) : queryable;
	var paginatedList = queryable.ToPaginatedList(pageIndex, pageSize);
	return paginatedList;
}
```

### Controller Sample
```
public class CountriesController : BaseApiController
{
	private readonly MultiTenantServices<Country, int> _countryService;
	public CountriesController(MultiTenantServices<Country, int> countryService, IMapper mapper, IUserContextDataProvider userContext) : base(mapper, userContext)
	{
		_countryService = countryService;
	}
	//... other implementations hidden
}
```

### Getting the TenantId
```
public async Task<PaginatedDto<CountryDto>> GetCountries(int pageIndex, int pageSize)
{
	PaginatedList<Country> countries = await _countryService.SearchAsync(new CountrySearchCondition
	{
		PageNo = pageIndex,
		RecordsPerPage = pageSize,
		TenantId = TenantId //This value is automatically picked from the GroupSid claim and set from the BaseApiController
	});
	PaginatedDto<CountryDto> countryPaginatedDto = _mapper.Map<PaginatedList<Country>, PaginatedDto<CountryDto>>(countries);
	return countryPaginatedDto;
	//return DtoResult<Country, CountryDto>(countries);
}
```

### Service Definition
```
public class CountryService : MultiTenantServices<Country, int>
{
	IMultiTenantRepository<Country, int> _repository = null;

	public CountryService(IMultiTenantRepository<Country, int> repository) : base(repository)
	{
		_repository = repository;
	}
	
	public override async Task<PaginatedList<Country>> SearchAsync(BaseSearchCondition<int> searchCondition)
	{
		if (searchCondition == null) return null;

		var result = await _repository.PaginateAsync(searchCondition.TenantId, searchCondition.PageNo, searchCondition.RecordsPerPage);
		return result;
	}	
	//other implementations are hidden for brevity
}
```
