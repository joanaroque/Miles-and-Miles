using CinelAirMiles.Prism.Helpers;
using CinelAirMiles.Prism.Models;
using CinelAirMiles.Prism.Services;
using CinelAirMiles.Prism.ViewModels;
using CinelAirMiles.Prism.Views;

using Newtonsoft.Json;

using Prism;
using Prism.Ioc;

using System;

using Xamarin.Forms;


namespace CinelAirMiles.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/CinelAirMilesMasterDetailPage/NavigationPage/LoginPage");
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }

            await NavigationService.NavigateAsync("CinelAirMilesMasterDetailPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            //containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            //containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            //containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            //containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            //containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            //containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
