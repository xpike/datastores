namespace XPike.DataStores
{
    /// <summary>
    /// Represents a class which manages database connection string, such
    /// as by replacing a connection name with a corresponding connection
    /// string obtained from application configuration files.
    /// </summary>
    public interface IConnectionStringManager
    {
        /// <summary>
        /// Gets the managed version of the specified connection string.
        /// </summary>
        /// <param name="connectionString">The manager-specific connection string, such as the name of a configuration key.</param>
        /// <returns>The managed connection string that corresponds to the specified value.</returns>
        string GetManagedConnectionString(string connectionString);
    }
}