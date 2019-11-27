using XPike.IoC;

namespace XPike.DataStores.MsSql
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.LoadPackage(new XPike.DataStores.Package());

            dependencyCollection.RegisterSingleton<IMsSqlDataConnectionProvider, MsSqlDataConnectionProvider>();
        }
    }
}