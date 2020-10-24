namespace CinelAirMiles.Controllers
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.AspNetCore.Mvc;

    public class ComplaintController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClientComplaint complaint)
        {
            return View();
        }
    }
}
