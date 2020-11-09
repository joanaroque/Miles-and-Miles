using Microsoft.AspNetCore.Mvc;

namespace CinelAirMiles.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult IndexClient()
        {
            return View();
        }

    }
}
