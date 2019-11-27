namespace XPike.DataStores
{
    /// <inheritdoc cref="IConnectionStringManager" />
    /// <summary>
    /// Represents a Connection String Manager which allows a DataStore to specify
    /// the name of a configuration key as its connection string, from which
    /// the actual connection string will be retrieved.
    /// </summary>
    public interface ISettingsConnectionStringManager
        : IConnectionStringManager
    {
    }
}