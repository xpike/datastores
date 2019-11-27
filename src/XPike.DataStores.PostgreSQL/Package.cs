using XPike.IoC;

namespace XPike.DataStores.PostgreSQL
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.AddXPikeDataStores();

            dependencyCollection.RegisterSingleton<IPostgreSQLDataConnectionProvider, PostgreSQLDataConnectionProvider>();
        }
    }
}