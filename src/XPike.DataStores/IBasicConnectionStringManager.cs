namespace XPike.DataStores
{
    /// <inheritdoc cref="IConnectionStringManager" />
    /// <summary>
    /// Represents a basic connection string manager, which simply returns
    /// the raw connection string value that was specified in the DataStore.
    /// </summary>
    public interface IBasicConnectionStringManager
        : IConnectionStringManager
    {
    }
}