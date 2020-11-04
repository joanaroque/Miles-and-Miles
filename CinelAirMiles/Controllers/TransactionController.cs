namespace CinelAirMiles.Controllers
{
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;

    public class TransactionController : Controller
    {

        public TransactionController()
        {

        }

        [HttpGet]
        public IActionResult MilesIndex()
        {
            return View();
        }


        [HttpGet]
        public IActionResult TransactionsIndex()
        {
            return View();
        }


        [HttpGet]
        public IActionResult PurchaseMiles()
        {
            return PartialView("_Purchase");
        }

        [HttpGet]
        public IActionResult ExtendMiles()
        {
            return PartialView("_Extend");
        }

        [HttpGet]
        public IActionResult ConvertMiles()
        {
            return PartialView("_Conversion");
        }


        [HttpGet]
        public IActionResult NominateToGold()
        {
            return PartialView();
        }


    }
}
