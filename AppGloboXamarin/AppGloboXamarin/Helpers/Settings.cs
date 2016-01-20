// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace AppGloboXamarin.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string IsLoggedKey = "IsLoggedKey";
        private static readonly bool IsLoggedDefault = false;

        private const string CreatedByKey = "CreatedByKey";
        private static readonly Guid CreatedByDefault = Guid.Empty;

        #endregion


        public static bool IsLogged
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsLoggedKey, IsLoggedDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsLoggedKey, value);
            }
        }

        public static Guid CreatedBy
        {
            get
            {
                return AppSettings.GetValueOrDefault<Guid>(CreatedByKey, CreatedByDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<Guid>(CreatedByKey, value);
            }
        }

    }
}