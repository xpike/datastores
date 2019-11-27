using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// Represents an IDataConnection to an Entity Framework Core DbContext.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext reference wrapped by this IDataConnection.</typeparam>
    public interface IEntityFrameworkCoreDataConnection<out TContext>
        : IDataConnection<TContext>
        where TContext : DbContext
    {
    }
}