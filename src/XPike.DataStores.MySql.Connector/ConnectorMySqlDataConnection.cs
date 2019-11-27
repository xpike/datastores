using MySql.Data.MySqlClient;

namespace XPike.DataStores.MySql.Connector
{
    public class ConnectorMySqlDataConnection
        : SqlDataConnectionBase,
          IConnectorMySqlDataConnection
    {
        public ConnectorMySqlDataConnection(MySqlConnection connection)
            : base(connection)
        {
        }
    }
}