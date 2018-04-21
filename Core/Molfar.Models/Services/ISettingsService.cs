namespace Molfar.Models.Services
{
    public interface ISettingsService
    {
        string GetSetting(string key);
        bool SaveSetting(string key, string value);
    }
}
