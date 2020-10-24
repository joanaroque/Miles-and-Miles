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
        public IActionResult TransactionIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            return PartialView("_Purchase");
        }

        [HttpGet]
        public IActionResult Extend()
        {
            return PartialView("_Extend");
        }

        [HttpGet]
        public IActionResult Conversion()
        {
            return PartialView("_Conversion");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTransaction(TransactionViewModel model)
        {
            return View();
        }



        [HttpGet]
        public IActionResult NominateToGold()
        {
            return PartialView();
        }


    }
}
