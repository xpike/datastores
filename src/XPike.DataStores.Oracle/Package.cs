using XPike.IoC;

namespace XPike.DataStores.Oracle
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.AddXPikeDataStores();

            dependencyCollection.RegisterSingleton<IOracleDataConnectionProvider, OracleDataConnectionProvider>();
        }
    }
}