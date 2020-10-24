namespace CinelAirMiles.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(Models.ComplaintViewModel model)
        {
            return View();
        }
    }
}
