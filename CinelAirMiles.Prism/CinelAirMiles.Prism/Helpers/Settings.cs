using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CinelAirMiles.Prism.Helpers
{
    public static class Settings
    {
        private const string _token = "Token";
        private const string _client = "Client";
        private const string _isRemembered = "IsRemembered";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;


        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Client
        {
            get => AppSettings.GetValueOrDefault(_client, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_client, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
    }
}
