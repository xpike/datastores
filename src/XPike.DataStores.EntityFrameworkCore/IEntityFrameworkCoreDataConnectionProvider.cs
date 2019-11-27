using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// Represents an IDataConnectionProvider which returns an IDataConnection that
    /// contains an Entity Framework Core DbContext.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext that is provided.</typeparam>
    public interface IEntityFrameworkCoreDataConnectionProvider<TContext>
        : IDataConnectionProvider<TContext>
        where TContext : DbContext
    {
    }
}