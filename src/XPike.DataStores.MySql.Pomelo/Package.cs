using XPike.IoC;

namespace XPike.DataStores.MySql.Pomelo
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.AddXPikeDataStores();

            dependencyCollection.RegisterSingleton<IPomeloMySqlDataConnectionProvider, PomeloMySqlDataConnectionProvider>();
 
            dependencyCollection.RegisterSingleton<IMySqlDataConnectionProvider>(container =>
                                                                                     container.ResolveDependency<IPomeloMySqlDataConnectionProvider>());
        }
    }
}