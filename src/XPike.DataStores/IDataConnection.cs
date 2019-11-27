namespace XPike.DataStores
{
    /// <summary>
    /// An abstract interface for classification purposes, which represents
    /// a type of connection to an underlying data storage system.
    /// </summary>
    public interface IDataConnection
    {
    }

    /// <inheritdoc cref="IDataConnection" />
    /// <summary>
    /// A connection-type-specific interface representing a particular type of connection
    /// (such as IDbConnection) to an underlying data storage system.
    /// </summary>
    /// <typeparam name="TConnection">The type of connection that is managed by this instance.</typeparam>
    public interface IDataConnection<out TConnection>
        : IDataConnection
        where TConnection : class
    {
        /// <summary>
        /// The connection object wrapped by this instance.
        /// </summary>
        TConnection Connection { get; }
    }
}