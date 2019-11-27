using System.Data;

namespace XPike.DataStores
{
    /// <inheritdoc cref="IDataConnectionProvider{TConnection}" />
    /// <summary>
    /// Represents an IDataConnectionProvider which returns a connection to a SQL server.
    /// </summary>
    public interface ISqlDataConnectionProvider
        : IDataConnectionProvider<IDbConnection>
    {
    }
}