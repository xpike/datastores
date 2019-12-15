using XPike.IoC;

namespace XPike.DataStores
{
    /// <inheritdoc cref="IDependencyPackage" />
    /// <summary>
    /// Dependency package which registers XPike.DataStores with a DI provider.
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        /// <inheritdoc />
        /// <summary>
        /// Registers XPike.DataStores with a DI provider.
        /// </summary>
        /// <param name="dependencyCollection">IDependencyCollection</param>
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            /* Load XPike.Logging and XPike.Configuration, which we depend on */
            dependencyCollection.LoadPackage(new XPike.Logging.Package());
            dependencyCollection.LoadPackage(new XPike.Configuration.Package());

            /* Register the Basic and Settings Connection String Managers */
            dependencyCollection.RegisterSingleton<IBasicConnectionStringManager, BasicConnectionStringManager>();
            dependencyCollection.RegisterSingleton<ISettingsConnectionStringManager, ConfigurationConnectionStringManager>();

            /* Proxy IConnectionStringManager to use IBasicConnectionStringManager by default */
            dependencyCollection.RegisterSingleton<IConnectionStringManager>(collection =>
                collection.ResolveDependency<IBasicConnectionStringManager>());
        }
    }
}