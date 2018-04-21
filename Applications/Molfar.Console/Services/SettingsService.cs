using Microsoft.Extensions.Configuration;
using Molfar.Models.Services;

namespace Molfar.Console.Services
{
    public class SettingsService : ISettingsService
    {
        IConfiguration _config;
        private const string MOLFAR_DEFAULT_SETTINGS = "MOLFAR_SETTINGS";
        public SettingsService()
        {
            // Adding JSON file into IConfiguration.
            _config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", true, true)
                 .Build();
        }
        public string GetSetting(string key)
        {
            string value = null;
            if (_config.GetSection(MOLFAR_DEFAULT_SETTINGS).Exists())
            {
                value = _config.GetSection(MOLFAR_DEFAULT_SETTINGS)[key];
            }
            return value;
        }

        public bool SaveSetting(string key, string value)
        {
            //Application.Current.Properties[key] = value;
            //return true;
            _config.GetSection(MOLFAR_DEFAULT_SETTINGS)[key] = value;
            return true;
        }
    }
}
