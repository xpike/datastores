using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace XPike.DataStores.MySql.Oracle
{
    public class OracleMySqlDataConnectionProvider 
        : IOracleMySqlDataConnectionProvider
    {
        public async Task<IDataConnection<IDbConnection>> GetConnectionAsync(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return new OracleMySqlDataConnection(connection);
        }
    }
}