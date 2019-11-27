using XPike.IoC;

namespace XPike.DataStores.Oracle
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikeOracleDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.Oracle.Package());
    }
}