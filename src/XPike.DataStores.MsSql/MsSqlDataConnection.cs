using System.Data.SqlClient;

namespace XPike.DataStores.MsSql
{
    public class MsSqlDataConnection
        : SqlDataConnectionBase,
          IMsSqlDataConnection
    {
        public MsSqlDataConnection(SqlConnection connection)
            : base(connection)
        {
        }
    }
}