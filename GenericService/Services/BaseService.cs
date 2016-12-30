using MultiTenantRepository;
using MultiTenantRepository.Entities;
using MultiTenantRepository.Enums;
using MultiTenantServices.ServiceExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantServices.Services
{
    /// <summary>
    /// The abstract generic service that contains the internals of the service implementations
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TId">The type of the entity identifier</typeparam>
    public abstract class MultiTenantServices<TEntity, TId>
        where TEntity : class, IMultiTenantEntity<TId>
        where TId : IComparable
    {
        private readonly IMultiTenantRepository<TEntity, TId> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantServices{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected MultiTenantServices(IMultiTenantRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        #region CRUD Operations
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="EntityValidationException">Entity data validation failed</exception>
        /// <exception cref="EntityPreProcessorExceptions">Entity preprocessing failed</exception>
        /// <exception cref="EntityPostProcessorExceptions">Entity postprocessing failed</exception>
        public virtual async Task<TId> AddAsync(TEntity entity)
        {
            try {
                IEnumerable<string> validationMessages = Enumerable.Empty<string>();
                if (!ValidateEntity(entity, EntityOperations.Create, out validationMessages)) {
                    throw new EntityValidationException("Entity data validation failed");
                }

                if (!ValidateACL(EntityOperations.Create, entity)) {
                    UnPrivilegedAccess(entity); return default(TId);
                }

                string message = string.Empty;
                if (!TryPreProcessEntity(entity, EntityOperations.Create, out message)) {
                    throw new EntityPreProcessorExceptions("Entity preprocessing failed");
                }

                await _repository.AddAsync(entity);
                int status = await _repository.SaveAsync();

                if (status != 0) {
                    OperationFailed(EntityOperations.Create, entity);
                    return default(TId);
                }
                if (!TryPostProcessEntity(EntityOperations.Create, out message, entity)) {
                    throw new EntityPostProcessorExceptions("Entity postprocessing failed");
                }
                return entity.Id;
            }
            catch (Exception exception) {
                ExceptionHandler(exception);
            }
            finally {
                CompleteServiceCall(EntityOperations.Create, entity);
            }
            return default(TId);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="EntityValidationException">Entity data validation failed</exception>
        /// <exception cref="EntityPreProcessorExceptions">Entity preprocessing failed</exception>
        /// <exception cref="EntityPostProcessorExceptions">Entity postprocessing failed</exception>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            try {
                IEnumerable<string> validationMessages = Enumerable.Empty<string>();
                if (!ValidateEntity(entity, EntityOperations.Update, out validationMessages)) {
                    throw new EntityValidationException("Entity data validation failed");
                }

                if (!ValidateACL(EntityOperations.Update, entity)) {
                    UnPrivilegedAccess(entity);
                    return int.MinValue;
                }

                string message = string.Empty;
                if (!TryPreProcessEntity(entity, EntityOperations.Update, out message)) {
                    throw new EntityPreProcessorExceptions("Entity preprocessing failed");
                }

                await _repository.EditAsync(entity);
                int status = await _repository.SaveAsync();

                if (status != 0) {
                    OperationFailed(EntityOperations.Update, entity);
                    return int.MinValue;
                }

                if (!TryPostProcessEntity(EntityOperations.Update, out message, entity)) {
                    throw new EntityPostProcessorExceptions("Entity postprocessing failed");
                }
                return status;
            }
            catch (Exception exception) {
                ExceptionHandler(exception);
            }
            finally {
                CompleteServiceCall(EntityOperations.Create, entity);
            }
            return -1;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        /// <exception cref="EntityValidationException">Entity data validation failed</exception>
        /// <exception cref="EntityPostProcessorExceptions">Entity postprocessing failed</exception>
        public virtual async Task<TEntity> GetByIdAsync(TId entityId)
        {
            TEntity entity = default(TEntity);
            try {
                var validationMessages = new Dictionary<string, string>();
                if (!ValidateEntityIds(EntityOperations.Read, out validationMessages, entityId)) {
                    throw new EntityValidationException("Entity data validation failed");
                }

                if (!ValidateACL(EntityOperations.Read, entity)) {
                    UnPrivilegedAccess(new[] { entityId }); return default(TEntity);
                }

                string message = string.Empty;

                entity = await _repository.GetSingleAsync(entityId);

                if (entity == null) {
                    OperationFailed(EntityOperations.Read, entity);
                    return default(TEntity);
                }

                if (!TryPostProcessEntity(EntityOperations.Read, out message, entity)) {
                    throw new EntityPostProcessorExceptions("Entity postprocessing failed");
                }
                return entity;
            }
            catch (Exception exception) {
                ExceptionHandler(exception);
            }
            finally {
                if (entity != default(TEntity))
                    CompleteServiceCall(EntityOperations.Create, entity);
            }
            return entity;
        }

        /// <summary>
        /// Gets the by ids asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="entityIds">The entity ids.</param>
        /// <returns></returns>
        /// <exception cref="EntityValidationException">Entity data validation failed</exception>
        /// <exception cref="EntityPostProcessorExceptions">Entity postprocessing failed</exception>
        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(Guid tenantId, IEnumerable<TId> entityIds)
        {
            IEnumerable<TEntity> entities = Enumerable.Empty<TEntity>();
            try {
                var validationMessages = new Dictionary<string, string>();
                if (!ValidateEntityIds(EntityOperations.Read, out validationMessages, entityIds.ToArray())) {
                    throw new EntityValidationException("Entity data validation failed");
                }

                if (!ValidateACL(EntityOperations.Read, entityIds.ToArray())) {
                    UnPrivilegedAccess(entityIds);
                    return Enumerable.Empty<TEntity>();
                }

                string message = string.Empty;

                entities = await _repository.GetByIdsAsync(tenantId, entityIds.ToArray());

                if (entities == null || entities.Count() < 1) {
                    OperationFailed(EntityOperations.Read, entities.ToArray());
                    return Enumerable.Empty<TEntity>();
                }

                if (!TryPostProcessEntity(EntityOperations.Read, out message, entities.ToArray())) {
                    throw new EntityPostProcessorExceptions("Entity postprocessing failed");
                }
                return entities;
            }
            catch (Exception exception) {
                ExceptionHandler(exception);
            }
            finally {
                if (entities != default(TEntity))
                    CompleteServiceCall(EntityOperations.Create, entities.ToArray());
            }
            return entities;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        /// <exception cref="EntityValidationException">Entity data validation failed</exception>
        /// <exception cref="EntityPostProcessorExceptions">Entity postprocessing failed</exception>
        public virtual async Task<int> DeleteAsync(TId entityId)
        {
            TEntity entity = default(TEntity);
            try {
                var validationMessages = new Dictionary<string, string>();
                if (!ValidateEntityIds(EntityOperations.Read, out validationMessages, entityId)) {
                    throw new EntityValidationException("Entity data validation failed");
                }

                if (!ValidateACL(EntityOperations.Update, entityId)) {
                    UnPrivilegedAccess(new[] { entityId }); return int.MinValue;
                }

                string message = string.Empty;

                entity = await GetByIdAsync(entityId);
                await _repository.DeleteAsync(entity);
                int status = await _repository.SaveAsync();

                if (status != 0) {
                    OperationFailed(EntityOperations.Update, entity);
                    return int.MinValue;
                }

                if (!TryPostProcessEntity(EntityOperations.Update, out message, entity)) {
                    throw new EntityPostProcessorExceptions("Entity postprocessing failed");
                }
                return status;
            }
            catch (Exception exception) {
                ExceptionHandler(exception);
            }
            finally {
                CompleteServiceCall(EntityOperations.Create, entity);
            }
            return -1;
        }

        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="searchCondition">The search condition.</param>
        /// <returns></returns>
        public abstract Task<PaginatedList<TEntity>> SearchAsync(BaseSearchCondition<TId> searchCondition);
        #endregion

        #region Entity related Input Validations
        /// <summary>
        /// Validates the entity ids.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="messages">The messages.</param>
        /// <param name="entityIds">The entity ids.</param>
        /// <returns></returns>
        protected abstract bool ValidateEntityIds(EntityOperations operation, out Dictionary<string, string> messages, params TId[] entityIds);
        /// <summary>
        /// Validates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        protected abstract bool ValidateEntity(TEntity entity, EntityOperations operation, out IEnumerable<string> messages);
        #endregion

        #region Pre / Post processing for Entity
        /// <summary>
        /// Tries the pre process entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected abstract bool TryPreProcessEntity(TEntity entity, EntityOperations operation, out string message);
        /// <summary>
        /// Tries the post process entity.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="message">The message.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected abstract bool TryPostProcessEntity(EntityOperations operation, out string message, params TEntity[] entity);
        #endregion

        #region Post Operations
        /// <summary>
        /// Completes the service call.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="entity">The entity.</param>
        protected abstract void CompleteServiceCall(EntityOperations operation, params TEntity[] entity);
        /// <summary>
        /// Uns the privileged access.
        /// </summary>
        /// <param name="entityIds">The entity ids.</param>
        protected abstract void UnPrivilegedAccess(IEnumerable<TId> entityIds);
        /// <summary>
        /// Uns the privileged access.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected abstract void UnPrivilegedAccess(TEntity entity);
        /// <summary>
        /// Operations the failed.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="entity">The entity.</param>
        protected abstract void OperationFailed(EntityOperations operation, params TEntity[] entity);
        /// <summary>
        /// Exceptions the handler.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public virtual void ExceptionHandler(Exception exception)
        {
            System.Diagnostics.Trace.WriteLine(exception);
        }
        #endregion

        #region Access Control Validations
        /// <summary>
        /// Validates the acl.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual bool ValidateACL(EntityOperations operation, params TEntity[] entity)
        {
            string privilegePrefis = string.Empty;
            switch (operation) {
                default:
                case EntityOperations.Read:
                    privilegePrefis = "View_";
                    break;
                case EntityOperations.Create:
                    privilegePrefis = "Add_";
                    break;
                case EntityOperations.Update:
                    privilegePrefis = "Edit_";
                    break;
                case EntityOperations.Delete:
                    privilegePrefis = "Delete_";
                    break;
                case EntityOperations.Search:
                    privilegePrefis = "Search_";
                    break;
            }

            return (1 == 1); // if user has privilege
        }
        /// <summary>
        /// Validates the acl.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="entityIds">The entity ids.</param>
        /// <returns></returns>
        public virtual bool ValidateACL(EntityOperations operation, params TId[] entityIds)
        {
            string privilegePrefis = string.Empty;
            switch (operation) {
                default:
                case EntityOperations.Read:
                    privilegePrefis = "View_";
                    break;
                case EntityOperations.Create:
                    privilegePrefis = "Add_";
                    break;
                case EntityOperations.Update:
                    privilegePrefis = "Edit_";
                    break;
                case EntityOperations.Delete:
                    privilegePrefis = "Delete_";
                    break;
                case EntityOperations.Search:
                    privilegePrefis = "Search_";
                    break;
            }

            return (1 == 1); // if user has privilege
        }
        #endregion

    }
}
