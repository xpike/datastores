using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace XPike.DataStores.PostgreSQL
{
    public class PostgreSQLDataConnectionProvider 
        : IPostgreSQLDataConnectionProvider
    {
        public async Task<IDataConnection<IDbConnection>> GetConnectionAsync(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return new PostgreSQLDataConnection(connection);
        }
    }
}