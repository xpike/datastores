namespace XPike.DataStores
{
    /// <inheritdoc cref="IBasicConnectionStringManager" />
    /// <summary>
    /// The default implementation of IBasicConnectionStringManager which returns
    /// the raw connection string value that was specified by the DatStore.
    /// </summary>
    public class BasicConnectionStringManager
        : IBasicConnectionStringManager
    {
        /// <inheritdoc />
        public virtual string GetManagedConnectionString(string connectionString) =>
            connectionString;
    }
}