using XPike.IoC;

namespace XPike.DataStores.MySql.Connector
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.AddXPikeDataStores();

            dependencyCollection.RegisterSingleton<IConnectorMySqlDataConnectionProvider, ConnectorMySqlDataConnectionProvider>();
 
            dependencyCollection.RegisterSingleton<IMySqlDataConnectionProvider>(container =>
                                                                                     container.ResolveDependency<IConnectorMySqlDataConnectionProvider>());
        }
    }
}