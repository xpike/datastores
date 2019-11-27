using System.Threading.Tasks;

namespace XPike.DataStores
{
    /// <summary>
    /// A purely decorative/categorical interface which represents a Data Connection Provider
    /// which is responsible for creating and opening a connection to a data source.
    /// </summary>
    public interface IDataConnectionProvider
    {
    }

    /// <inheritdoc cref="IDataConnection{TConnection}" />
    /// <summary>
    /// Represents a Data Connection Provider which can establish
    /// a specific type of connection a data source.
    /// </summary>
    /// <typeparam name="TConnection">The type of connection object to be created.</typeparam>
    public interface IDataConnectionProvider<TConnection>
        : IDataConnectionProvider
        where TConnection : class
    {
        /// <summary>
        /// Creates and opens a connection to a data source using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The actual prepared connection string necessary to establish a connection.</param>
        /// <returns>An open connection of type TConnection.</returns>
        Task<IDataConnection<TConnection>> GetConnectionAsync(string connectionString);
    }
}