using XPike.Settings;

namespace XPike.DataStores
{

    public class SettingsConnectionStringManager
        : ISettingsConnectionStringManager
    {
        private readonly ISettingsService _settingsService;

        public SettingsConnectionStringManager(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public string GetManagedConnectionString(string connectionString) =>
            _settingsService.GetValue(connectionString);
    }
}