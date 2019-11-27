using System.Data;

namespace XPike.DataStores
{
    /// <inheritdoc cref="IDataConnection{TConnection}" />
    /// <summary>
    /// Represents an IDataConnection to a SQL server.
    /// </summary>
    public interface ISqlDataConnection
        : IDataConnection<IDbConnection>
    {
    }
}