# Service Templates

## Cusomtization or Extension Of Services from the BaseService
- The services are the implementations that speak about the business contracts to the presentation or implementations
- The Business rules are typically applied or enforced here

The services have a base class that wires most of the required functionality. A typical service flow is outlined as follows
1. Validate the inputs
2. Check if the User has the privilege to invoke the service methods or access the repository
3. Perform the pre-processing of the data before passing them on to the repository. This may involve custom business rule processing before performing any operation on the repository
4. Invoke the repository and perform the CRUD
5. Invoke the post processing, i.e. after the data is fetched from the repository, the typical operations could be caching, or applying some business logic before the data is sent out from the service layer
6. Returning the caller
7. The transaction support is in development and will be applied in the next release of the template.

### Service Skeleton
```
public virtual async Task<TId> AddAsync(TEntity entity)
{
	try
	{
		IEnumerable<string> validationMessages = Enumerable.Empty<string>();
		//Input data validations go here
		if (!ValidateEntity(entity, EntityOperations.Create, out validationMessages))
		{
			throw new EntityValidationException("Entity data validation failed");
		}
		//Access permission checks go here
		if (!ValidateACL(EntityOperations.Create, entity))
		{
			UnPrivilegedAccess(entity);
		}

		//pre-processing of the data is done here
		string message = string.Empty;
		if (!TryPreProcessEntity(entity, EntityOperations.Create, out message))
		{
			throw new EntityPreProcessorExceptions("Entity preprocessing failed");
		}
		//Repository call
		await _repository.AddAsync(entity);
		int status = await _repository.SaveAsync();

		//Status reporting if any failure
		if (status != 0) OperationFailed(EntityOperations.Create, entity);

		//Post-processing of the data
		if (!TryPostProcessEntity(EntityOperations.Create, out message, entity))
		{
			throw new EntityPostProcessorExceptions("Entity postprocessing failed");
		}
		return entity.Id;
	}
	catch (Exception exception)
	{
		ExceptionHandler(exception); //Extendible exception handler in the implementation
	}
	finally
	{
		CompleteServiceCall(EntityOperations.Create, entity); // final cleanup hook customizable in the service implementation
	}
	return default(TId);
}
```
