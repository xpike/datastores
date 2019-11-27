using MySql.Data.MySqlClient;

namespace XPike.DataStores.MySql.Oracle
{
    public class OracleMySqlDataConnection
        : SqlDataConnectionBase,
          IOracleMySqlDataConnection
    {
        public OracleMySqlDataConnection(MySqlConnection connection)
            : base(connection)
        {
        }
    }
}