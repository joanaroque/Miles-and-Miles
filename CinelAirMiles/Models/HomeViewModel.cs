using System.Collections.Generic;

namespace CinelAirMiles.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.RegisterViewModel = new RegisterNewUserViewModel();
            this.AdvertisingViewModel = new List<AdvertisingViewModel>();
        }

        public RegisterNewUserViewModel RegisterViewModel { get; set; }

        public List<AdvertisingViewModel> AdvertisingViewModel { get; set; }
    }
}
