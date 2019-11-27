namespace XPike.DataStores
{
    /// <summary>
    /// A purely decorative/categorical interface that represents a Data Store,
    /// which is a class responsible for interacting with a data source.
    /// </summary>
    public interface IDataStore
    {
    }

    /// <inheritdoc cref="IDataStore" />
    /// <summary>
    /// Represents a Data Store that relies on a connection of type TConnection.
    /// </summary>
    /// <typeparam name="TConnection">The type of connection necessary, such as IDbConnection.</typeparam>
    public interface IDataStore<TConnection>
        : IDataStore
        where TConnection : class
    {
    }
}