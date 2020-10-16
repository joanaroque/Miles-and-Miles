using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace MilesBackOffice.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult PremiumIndex()
        {
            return View();
        }


        public IActionResult NewsIndex()
        {
            return View();
        }


        public IActionResult PartnerIndex()
        {
            return View();
        }
    }
}
