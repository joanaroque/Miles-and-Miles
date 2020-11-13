using System.Globalization;
using CinelAirMiles.Prism.Interfaces;
using Xamarin.Forms;
using CinelAirMiles.Prism.Resources;

namespace CinelAirMiles.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string Loading => Resource.Loading;

        public static string Name => Resource.Name;

        public static string Login => Resource.Login;

        public static string LoginError => Resource.LoginError;

        public static string Email => Resource.Email;

        public static string EmailError => Resource.EmailError;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Forgot => Resource.Forgot;

        public static string Register => Resource.Register;

        public static string Rememberme => Resource.Rememberme;

        public static string Saving => Resource.Saving;

        public static string Save => Resource.Save;

        public static string Created => Resource.Created;

        public static string Edited => Resource.Edited;

        public static string Confirm => Resource.Confirm;

        public static string Delete => Resource.Delete;

        public static string Yes => Resource.Yes;

        public static string No => Resource.No;

        public static string Ok => Resource.Ok;
    }
}
