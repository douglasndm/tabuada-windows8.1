using System;
using Windows.Storage;

namespace Tabuada.Class
{
    class AppSettings
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public bool GetBoolSettings(string SettingName)
        {
            return Convert.ToBoolean(localSettings.Values[SettingName]);
        }

        public int GetIntSetting(string SettingName)
        {
            return Convert.ToInt32(localSettings.Values[SettingName]);
        }



        public void UpdateBoolSettings(string SettingName, bool SettingValue)
        {
            localSettings.Values[SettingName] = SettingValue;
        }

        public void UpdateIntSettings(string SettingName, int SettingValue)
        {
            localSettings.Values[SettingName] = SettingValue;
        }
    }
}
