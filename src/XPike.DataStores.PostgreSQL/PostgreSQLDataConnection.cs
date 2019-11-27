using Npgsql;

namespace XPike.DataStores.PostgreSQL
{
    public class PostgreSQLDataConnection
        : SqlDataConnectionBase,
          IPostgreSQLDataConnection
    {
        public PostgreSQLDataConnection(NpgsqlConnection connection)
            : base(connection)
        {
        }
    }
}