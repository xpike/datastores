using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// An abstract implementation of an Entity Framework Core DataStore.
    /// </summary>
    /// <typeparam name="TContext">The type of DbContext that will be used.</typeparam>
    public abstract class EntityFrameworkCoreDataStoreBase<TContext>
        : DataStoreBase<TContext>,
          IEntityFrameworkCoreDataStore<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Creates a new instance of EntityFrameworkDataStoreBase.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connectionStringManager"></param>
        protected EntityFrameworkCoreDataStoreBase(IEntityFrameworkCoreDataConnectionProvider<TContext> provider, IConnectionStringManager connectionStringManager)
            : base(provider, connectionStringManager)
        {
        }

        /// <summary>
        /// A helper function to safely acquire a DbContext and execute a command against it.
        /// It's recommended that all DataStore operations use this.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned from the operation.</typeparam>
        /// <param name="callback">The callback to execute which uses the DbContext and returns the result.</param>
        /// <returns>The object of type TResult which is returned by the callback.</returns>
        protected Task<TResult> WithContextAsync<TResult>(Func<TContext, Task<TResult>> callback) =>
            base.WithConnectionAsync(connection => callback(connection.Connection));
    }
}