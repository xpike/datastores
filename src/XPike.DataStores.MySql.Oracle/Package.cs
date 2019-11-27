using XPike.IoC;

namespace XPike.DataStores.MySql.Oracle
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.AddXPikeDataStores();

            dependencyCollection.RegisterSingleton<IOracleMySqlDataConnectionProvider, OracleMySqlDataConnectionProvider>();
 
            dependencyCollection.RegisterSingleton<IMySqlDataConnectionProvider>(container =>
                                                                                     container.ResolveDependency<IOracleMySqlDataConnectionProvider>());
        }
    }
}