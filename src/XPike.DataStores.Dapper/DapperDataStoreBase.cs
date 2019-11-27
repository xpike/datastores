namespace XPike.DataStores.Dapper
{
    /// <inheritdoc cref="SqlDataStoreBase" />
    /// <inheritdoc cref="IDapperDataStore" />
    /// <summary>
    /// An abstract implementation of a SQL DataStore which uses Dapper.
    /// </summary>
    public abstract class DapperDataStoreBase
        : SqlDataStoreBase,
          IDapperDataStore
    {
        /// <summary>
        /// Creates a new instance of DapperDataStoreBase which uses the specified ISqlDataConnectionProvider to
        /// establish a connection to the database, and uses the specified IConnectionStringManager to obtain the
        /// connection string to use by passing in the base class's ConnectionString property.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connectionStringManager"></param>
        protected DapperDataStoreBase(ISqlDataConnectionProvider provider, IConnectionStringManager connectionStringManager)
            : base(provider, connectionStringManager)
        {
        }
    }
}