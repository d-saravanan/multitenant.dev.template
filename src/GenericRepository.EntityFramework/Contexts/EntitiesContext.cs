using MultiTenantRepository.Shards.Contracts;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace MultiTenantRepository.EntityFramework
{
    /// <summary>
    /// The abstract  entity context class
    /// </summary>
    public abstract class EntitiesContext : DbContext, IEntitiesContext
    {
        /// <summary>
        /// Constructs a new context instance using conventions to create the name of
        /// the database to which a connection will be made. The by-convention name is
        /// the full name (namespace + class name) of the derived context class.  See
        /// the class remarks for how this is used to create a connection. 
        /// </summary>
        protected EntitiesContext() : base()
        {
        }

        /// <summary>
        /// Initializes the entity context based on the established user context and the tenant shard map resolver
        /// </summary>
        /// <param name="userContext">The user context</param>
        /// <param name="shardResolver">The Tenant Shard map resolver</param>
        /// <param name="nameorConnectionString">The name or the connection string for the entity</param>
        protected EntitiesContext(MultiTenancy.Core.ProviderContracts.IUserContextDataProvider userContext, ITenantShardResolver shardResolver, string nameorConnectionString)
            : base(shardResolver.GetConnection(userContext.TenantId, nameorConnectionString), true)
        {

        }

        /// <summary>
        /// Constructs a new context instance using conventions to create the name of
        /// the database to which a connection will be made, and initializes it from
        /// the given model.  The by-convention name is the full name (namespace + class
        /// name) of the derived context class.  See the class remarks for how this is
        /// used to create a connection.
        /// </summary>
        /// <param name="model">The model that will back this context.</param>
        protected EntitiesContext(DbCompiledModel model) : base(model)
        {
        }

        /// <summary>
        /// Constructs a new context instance using the given string as the name or connection
        /// string for the database to which a connection will be made.  See the class
        /// remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public EntitiesContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Constructs a new context instance using the existing connection to connect
        /// to a database.  The connection will not be disposed when the context is disposed.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">
        /// If set to true the connection is disposed when the context is disposed, otherwise
        /// the caller must dispose the connection.
        /// </param>
        public EntitiesContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Constructs a new context instance around an existing ObjectContext.  An existing
        /// ObjectContext to wrap with the new context.  If set to true the ObjectContext
        /// is disposed when the EntitiesContext is disposed, otherwise the caller must dispose
        /// the connection.
        /// </summary>
        /// <param name="objectContext">An existing ObjectContext to wrap with the new context.</param>
        /// <param name="EntitiesContextOwnsObjectContext">
        /// If set to true the ObjectContext is disposed when the EntitiesContext is disposed,
        /// otherwise the caller must dispose the connection.
        /// </param>
        public EntitiesContext(ObjectContext objectContext, bool EntitiesContextOwnsObjectContext)
            : base(objectContext, EntitiesContextOwnsObjectContext)
        {
        }

        /// <summary>
        /// Constructs a new context instance using the given string as the name or connection
        /// string for the database to which a connection will be made, and initializes
        /// it from the given model.  See the class remarks for how this is used to create
        /// a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public EntitiesContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        /// Constructs a new context instance using the existing connection to connect
        /// to a database, and initializes it from the given model.  The connection will
        /// not be disposed when the context is disposed.  An existing connection to
        /// use for the new context.  The model that will back this context.  If set
        /// to true the connection is disposed when the context is disposed, otherwise
        /// the caller must dispose the connection.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="model">The model that will back this context.</param>
        /// <param name="contextOwnsConnection">
        /// If set to true the connection is disposed when the context is disposed, otherwise
        /// the caller must dispose the connection.
        /// </param>
        public EntitiesContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Sets the entity in the db set
        /// </summary>
        /// <typeparam name="TEntity">The entity to be set</typeparam>
        /// <returns>The dbset</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Sets the entity state as added
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="entity">the entity</param>
        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : class
        {

            DbEntityEntry dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        /// <summary>
        /// Sets the entity state as modified
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="entity">the entity</param>
        public void SetAsModified<TEntity>(TEntity entity) where TEntity : class
        {

            DbEntityEntry dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Sets the entity state as deleted
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="entity">the entity</param>
        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {

            DbEntityEntry dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        // privates
        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : class
        {

            DbEntityEntry dbEntityEntry = base.Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {

                Set<TEntity>().Attach(entity);
            }

            return dbEntityEntry;
        }
    }
}