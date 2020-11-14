using Prism.Commands;
using Prism.Navigation;
using CinelAirMiles.Prism.Models;
using CinelAirMiles.Prism.Helpers;


namespace CinelAirMiles.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;


        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public DelegateCommand SelectMenuCommand =>
            _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));


        private async void SelectMenuAsync()
        {
            if (PageName.Equals("LoginPage"))
            {
                Settings.IsRemembered = false;
                await _navigationService.NavigateAsync("/NavigationPage/LoginPage");
                return;
            }

            await _navigationService.NavigateAsync($"/{nameof(CinelAirMilesMasterDetailPage)}/NavigationPage/{PageName}");
        }
    }
}
