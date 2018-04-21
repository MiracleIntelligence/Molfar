using Molfar.Models.Services;
using System;
using Xamarin.Forms;

namespace Molfar.Core.Services
{
    public class SettingsService : ISettingsService
    {
        public string GetSetting(string key)
        {
            string value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = (string)Application.Current.Properties[key];
                // do something with id
            }

            return value;
        }

        public bool SaveSetting(string key, string value)
        {
            Application.Current.Properties[key] = value;
            Application.Current.SavePropertiesAsync();

            return true;
        }
    }
}
