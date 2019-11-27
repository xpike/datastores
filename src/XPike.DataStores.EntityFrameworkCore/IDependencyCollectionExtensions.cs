using XPike.IoC;

namespace XPike.DataStores.EntityFrameworkCore
{
    /// <summary>
    /// Registers XPike.DataStores.EntityFramework with a DI Provider.
    /// </summary>
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Registers XPike.DataStores.EntityFramework with the specified IDependencyCollection.
        /// </summary>
        /// <param name="collection">The IDependencyCollection to register with.</param>
        /// <returns>The IDependencyCollection.</returns>
        public static IDependencyCollection AddXPikeEntityFrameworkCoreDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.EntityFrameworkCore.Package());
    }
}