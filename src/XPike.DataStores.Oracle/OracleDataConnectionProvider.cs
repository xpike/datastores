using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace XPike.DataStores.Oracle
{
    public class OracleDataConnectionProvider 
        : IOracleDataConnectionProvider
    {
        public async Task<IDataConnection<IDbConnection>> GetConnectionAsync(string connectionString)
        {
            var connection = new OracleConnection(connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return new OracleDataConnection(connection);
        }
    }
}