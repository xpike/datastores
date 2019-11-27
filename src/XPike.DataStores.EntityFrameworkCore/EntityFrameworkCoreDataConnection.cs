using Microsoft.EntityFrameworkCore;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// A basic implementation of an IDataConnection for Entity Framework Core.
    /// </summary>
    /// <typeparam name="TContext">The DbContext type which will be used.</typeparam>
    public class EntityFrameworkCoreDataConnection<TContext>
        : DataConnectionBase<TContext>,
          IEntityFrameworkCoreDataConnection<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Creates a new EntityFrameworkCoreDataConnection.
        /// </summary>
        /// <param name="context">The DbContext instance to use.</param>
        public EntityFrameworkCoreDataConnection(TContext context)
            : base(context)
        {
        }
    }
}