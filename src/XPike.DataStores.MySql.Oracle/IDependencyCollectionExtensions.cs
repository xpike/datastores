using XPike.IoC;

namespace XPike.DataStores.MySql.Oracle
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikeOracleMySqlDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.MySql.Oracle.Package());
    }
}