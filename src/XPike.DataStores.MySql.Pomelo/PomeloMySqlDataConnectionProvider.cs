using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace XPike.DataStores.MySql.Pomelo
{
    public class PomeloMySqlDataConnectionProvider
        : IPomeloMySqlDataConnectionProvider
    {
        public async Task<IDataConnection<IDbConnection>> GetConnectionAsync(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return new PomeloMySqlDataConnection(connection);
        }
    }
}