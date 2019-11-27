using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// An IDataConnectionProvider for Entity Framework Core.
    /// </summary>
    /// <typeparam name="TContext">The type of DbContext which will be returned in the IDataConnection.</typeparam>
    public class EntityFrameworkCoreDataConnectionProvider<TContext>
        : IEntityFrameworkCoreDataConnectionProvider<TContext>
        where TContext : DbContext
    {
        private readonly IEntityFrameworkCoreDataConnection<TContext> _connection;

        /// <summary>
        /// Creates a new Entity Framework Core Data Connection Provider.
        /// </summary>
        /// <param name="connection">The instance of IEntityFramworkDataConnection to use.</param>
        public EntityFrameworkCoreDataConnectionProvider(IEntityFrameworkCoreDataConnection<TContext> connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Obtains a connection, which is just a wrapper around the DbContext type specified by TContext.
        /// 
        /// NOTE: The "connectionString" parameter is NOT used by this provider at this time.
        ///       Specify your connection string using either standard Entity Framework Core semantics, or
        ///       by injecting an IConnectionStringManager (or a derived interface) into your DbContext and
        ///       configure the connection in the OnConfiguring() method.
        /// </summary>
        /// <param name="connectionString">Not currently used by this Entity Framework Core provider.</param>
        /// <returns>The DbContext wrapped in an IDataConnection.</returns>
        public Task<IDataConnection<TContext>> GetConnectionAsync(string connectionString) =>
            Task.FromResult<IDataConnection<TContext>>(_connection);
    }
}