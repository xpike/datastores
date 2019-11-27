using XPike.IoC;

namespace XPikeDataStores
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection AddXPikeDataStoresExample(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPikeDataStores.Package());
    }
}