using XPike.Configuration;

namespace XPike.DataStores
{
    public class ConfigurationConnectionStringManager
        : ISettingsConnectionStringManager
    {
        private readonly IConfigurationService _configService;

        public ConfigurationConnectionStringManager(IConfigurationService configService)
        {
            _configService = configService;
        }

        public string GetManagedConnectionString(string connectionString) =>
            _configService.GetValue(connectionString);
    }
}