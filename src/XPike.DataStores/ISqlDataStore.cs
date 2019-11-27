using System.Data;

namespace XPike.DataStores
{
    /// <inheritdoc cref="IDataStore{TConnection}" />
    /// <summary>
    /// Represents an IDataStore which connects to a SQL server.
    /// </summary>
    public interface ISqlDataStore
        : IDataStore<IDbConnection>
    {
    }
}