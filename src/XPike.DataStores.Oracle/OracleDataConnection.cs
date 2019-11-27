using Oracle.ManagedDataAccess.Client;

namespace XPike.DataStores.Oracle
{
    public class OracleDataConnection
        : SqlDataConnectionBase,
          IOracleDataConnection
    {
        public OracleDataConnection(OracleConnection connection)
            : base(connection)
        {
        }
    }
}