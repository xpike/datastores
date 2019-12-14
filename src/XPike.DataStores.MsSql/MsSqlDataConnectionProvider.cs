using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace XPike.DataStores.MsSql
{
    public class MsSqlDataConnectionProvider 
        : IMsSqlDataConnectionProvider
    {
        public async Task<IDataConnection<IDbConnection>> GetConnectionAsync(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return new MsSqlDataConnection(connection);
        }
    }
}