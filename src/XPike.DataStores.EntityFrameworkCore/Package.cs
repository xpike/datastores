using XPike.IoC;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// Dependency package to register XPike.DataStores.EntityFrameworkCore with a DI Provider.
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        /// <summary>
        /// Registers XPike.DataStores.EntityFrameworkCore with the specified IDependencyCollection.
        /// </summary>
        /// <param name="dependencyCollection">The IDependencyCollection to register with.</param>
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            /* Load the XPike.DataStores package which we depend on */
            dependencyCollection.LoadPackage(new XPike.DataStores.Package());

            /* Registers IEntityFrameworkDataConnectionProvider as a scoped open generic so it will automatically handle any DbContext */
            dependencyCollection.RegisterScoped(typeof(IEntityFrameworkCoreDataConnectionProvider<>),
                                                           typeof(EntityFrameworkCoreDataConnectionProvider<>));

            /* Registers IEntityFrameworkCoreDataConnection as a scoped open generic so that it will automatically handle any DbContext */
            dependencyCollection.RegisterScoped(typeof(IEntityFrameworkCoreDataConnection<>),
                typeof(EntityFrameworkCoreDataConnection<>));
        }
    }
}