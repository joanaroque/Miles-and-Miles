using Microsoft.AspNetCore.Mvc;

namespace CinelAirMiles.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult IndexClient()
        {
            return View();
        }


        [Route("error/404")]
        public IActionResult Error404Client()
        {
            return View();
        }

        [Route("error/500")]
        public IActionResult Error500Client()
        {
            return View();
        }

    }
}
