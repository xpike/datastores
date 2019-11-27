namespace XPike.DataStores
{
    /// <inheritdoc cref="IDataConnection{TConnection}" />
    /// <summary>
    /// A simple abstract implementation of IDataConnection which
    /// should be suitable for use by most providers.
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection reference to hold.</typeparam>
    public abstract class DataConnectionBase<TConnection>
        : IDataConnection<TConnection>
        where TConnection : class
    {
        /// <inheritdoc />
        public TConnection Connection { get; protected set; }

        /// <summary>
        /// Creates a new DataConnectionBase which holds the specified connection.
        /// </summary>
        /// <param name="connection">The TConnection object to hold a reference to.</param>
        protected DataConnectionBase(TConnection connection)
        {
            Connection = connection;
        }
    }
}