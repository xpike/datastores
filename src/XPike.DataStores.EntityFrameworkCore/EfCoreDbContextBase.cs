using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    public abstract class EfCoreDbContextBase
        : DbContext
    {
        private readonly IConnectionStringManager _connectionManager;

        protected abstract string ConnectionString { get; }

        protected EfCoreDbContextBase(IConnectionStringManager connectionManager, DbContextOptions options)
            : base(options)
        {
            _connectionManager = connectionManager;
        }

        protected EfCoreDbContextBase(IConnectionStringManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        protected virtual string GetManagedConnectionString() =>
            _connectionManager.GetManagedConnectionString(ConnectionString);
    }
}