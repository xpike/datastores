using XPike.IoC;

namespace XPike.DataStores.MsSql
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikeMsSqlDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.MsSql.Package());
    }
}