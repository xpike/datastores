using System.Data;

namespace XPike.DataStores
{
    /// <inheritdoc cref="DataConnectionBase{TConnection}" />
    /// <inheritdoc cref="ISqlDataConnection" />
    /// <summary>
    /// An abstract implementation of ISqlDataConnection.
    /// </summary>
    public abstract class SqlDataConnectionBase
        : DataConnectionBase<IDbConnection>,
          ISqlDataConnection
    {
        /// <summary>
        /// Creates a new SqlDataConnection which uses the specified IDbConnection.
        /// </summary>
        /// <param name="connection">The IDbConnection to use when executing queries.</param>
        protected SqlDataConnectionBase(IDbConnection connection)
            : base(connection)
        {
        }
    }
}