using System;
using System.Threading.Tasks;

namespace XPike.DataStores
{
    /// <inheritdoc cref="IDataConnection{TConnection}" />
    /// <summary>
    /// An abstract implementation of an IDataStore.
    /// </summary>
    /// <typeparam name="TConnection">The type of connection required by this DataStore.</typeparam>
    public abstract class DataStoreBase<TConnection>
        : IDataStore<TConnection>
        where TConnection : class
    {
        private readonly IDataConnectionProvider<TConnection> _provider;
        private readonly IConnectionStringManager _connectionStringManager;

        /// <summary>
        /// Must be overridden in an implementation to specify the connection
        /// string to pass to the IConnectionStringManager.
        /// </summary>
        protected abstract string ConnectionString { get; }

        /// <summary>
        /// Creates a new DataStoreBase using the specified Connection Provider and Connection String Manager.
        /// </summary>
        /// <param name="provider">The provider to use to retrieve an instance of an IDataConnection / TConnection.</param>
        /// <param name="connectionStringManager">The connection string manager to use to convert the ConnectionString to an actual connection string.</param>
        protected DataStoreBase(IDataConnectionProvider<TConnection> provider, IConnectionStringManager connectionStringManager)
        {
            _provider = provider;
            _connectionStringManager = connectionStringManager;
        }

        /// <summary>
        /// Executes action asynchronously which uses the IDataConnection to obtain a result.
        /// </summary>
        /// <typeparam name="TResult">The type of result which will be returned.</typeparam>
        /// <param name="callback">The action to invoke asynchronously after obtaining a connection.</param>
        /// <returns>The result of the data access operation.</returns>
        protected virtual async Task<TResult> WithConnectionAsync<TResult>(Func<IDataConnection<TConnection>, Task<TResult>> callback)
        {
            var connection = await GetConnectionAsync();
            return await callback(connection);
        }

        /// <summary>
        /// Uses the Connection Provider to obtain a connection to the database.
        /// </summary>
        /// <returns>An established and open connection to the server.</returns>
        protected virtual Task<IDataConnection<TConnection>> GetConnectionAsync() =>
            _provider.GetConnectionAsync(_connectionStringManager.GetManagedConnectionString(ConnectionString));
    }
}