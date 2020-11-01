using CinelAirMiles.Prism.Helpers;
using CinelAirMiles.Prism.Models;
using CinelAirMiles.Prism.Views;

using Prism.Navigation;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CinelAirMiles.Prism.ViewModels
{
    public class CinelAirMilesMasterDetailPage : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public CinelAirMilesMasterDetailPage(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {

              new Menu
              {
                    Icon = "ic_map",
                    PageName = $"{nameof(MapPage)}",
                    Title = "Map"
              },

            new Menu
            {
                Icon = "ic_person",
                PageName = $"{nameof(ProfilePage)}",
                Title = "Modify Profile",
               IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = $"{nameof(LoginPage)}",
                Title = "Logout"
            }
};

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }
}
