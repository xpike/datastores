using System;
using System.Data;
using System.Threading.Tasks;

namespace XPike.DataStores
{
    /// <inheritdoc cref="DataStoreBase{TConnection}" />
    /// <inheritdoc cref="ISqlDataStore" />
    /// <summary>
    /// An abstract implementation of a SQL DataStore which uses Dapper.
    /// </summary>
    public abstract class SqlDataStoreBase
        : DataStoreBase<IDbConnection>,
          ISqlDataStore
    {
        /// <summary>
        /// Creates a new instance of SqlDataStoreBase which uses the specified ISqlDataConnectionProvider
        /// to obtain a connection to the database, and the specified IConnectionStringManager to obtain
        /// the actual connection string to use to establish that connection, by passing in the value of the
        /// base class ConnectionString property.
        /// </summary>
        /// <param name="provider">The ISqlDataConnectionProvider to use to establish a connection.</param>
        /// <param name="connectionStringManager">The IConnectionStringManager to use to convert the value from the ConnectionString property.</param>
        protected SqlDataStoreBase(ISqlDataConnectionProvider provider, IConnectionStringManager connectionStringManager)
            : base(provider, connectionStringManager)
        {
        }

        /// <summary>
        /// A helper function to safely acquire an IDbConnection and execute a command against it.
        /// It's recommended that all DataStore operations use this.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned from the operation.</typeparam>
        /// <param name="callback">The callback to execute which uses the IDbConnection and returns the result.</param>
        /// <returns>The object of type TResult which is returned by the callback.</returns>
        protected virtual Task<TResult> WithSqlConnectionAsync<TResult>(Func<IDbConnection, Task<TResult>> callback) =>
            base.WithConnectionAsync(connection => callback(connection.Connection));
    }
}