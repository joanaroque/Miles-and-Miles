using Prism.Navigation;

namespace CinelAirMiles.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Cinel Air Miles";
        }
    }
}
