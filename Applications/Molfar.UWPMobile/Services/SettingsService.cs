using Molfar.Models.Services;
using System;

namespace Molfar.UWPMobile.Services
{
    public class SettingsService : ISettingsService
    {
        public string GetSetting(string key)
        {
            string value = null;
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
            {
                value = (string)localSettings.Values[key];
            }

            return value;
        }

        public bool SaveSetting(string key, string value)
        {
            Windows.Storage.ApplicationDataContainer localSettings =
       Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[key] = value;

            return true;
        }
    }
}
