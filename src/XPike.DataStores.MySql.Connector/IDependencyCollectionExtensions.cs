using XPike.IoC;

namespace XPike.DataStores.MySql.Connector
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikeMySqlConnectorDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.MySql.Connector.Package());
    }
}