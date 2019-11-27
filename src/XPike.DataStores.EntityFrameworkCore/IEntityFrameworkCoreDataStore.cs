using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// Represents an IDataStore for Entity Framework Core.
    /// </summary>
    /// <typeparam name="TContext">The type of DbContext that will be used.</typeparam>
    public interface IEntityFrameworkCoreDataStore<TContext>
        : IDataStore<TContext>
        where TContext : DbContext
    {
    }
}