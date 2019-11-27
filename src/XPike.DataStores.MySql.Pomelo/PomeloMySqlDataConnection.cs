using MySql.Data.MySqlClient;

namespace XPike.DataStores.MySql.Pomelo
{
    public class PomeloMySqlDataConnection
        : SqlDataConnectionBase,
          IPomeloMySqlDataConnection
    {
        public PomeloMySqlDataConnection(MySqlConnection connection)
            : base(connection)
        {
        }
    }
}