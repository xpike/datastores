using XPike.IoC;

namespace XPike.DataStores
{
    /// <summary>
    /// Extension methods to register and configure the XPike.DataStores library.
    /// </summary>
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Registers necessary dependencies with the DI provider.
        /// </summary>
        /// <param name="collection">The IDependencyCollection to register with.</param>
        /// <returns>The IDependencyCollection.</returns>
        public static IDependencyCollection AddXPikeDataStores(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.DataStores.Package());
    }
}