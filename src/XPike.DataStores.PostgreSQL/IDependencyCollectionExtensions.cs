using XPike.IoC;

namespace XPike.DataStores.PostgreSQL
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikePostgreSQLDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.PostgreSQL.Package());
    }
}