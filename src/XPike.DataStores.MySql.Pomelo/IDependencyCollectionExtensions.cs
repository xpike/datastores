using XPike.IoC;

namespace XPike.DataStores.MySql.Pomelo
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikePomeloMySqlDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.MySql.Pomelo.Package());
    }
}