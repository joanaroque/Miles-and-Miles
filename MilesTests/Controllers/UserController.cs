namespace MilesBackOffice.Web.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Data.Repositories.User;
    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models.User;

    public class UserController : Controller
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converter;

        public UserController(IPremiumRepository premiumRepository,
            IUserHelper userHelper,
            IConverterHelper converter)
        {
            _premiumRepository = premiumRepository;
            _userHelper = userHelper;
            _converter = converter;
        }

        /// <summary>
        /// The list loaded should be only of the results that were sent back by the SU
        /// TODO The page should include a button that loads the entire datacontext of PremiumOffers as demand of the User
        /// </summary>
        /// <returns></returns>
        public IActionResult PremiumIndex()
        {
            var list = _premiumRepository.GetAllOffers();

            var model = new PremiumIndexViewModel
            {
                PremiumOffers = list
            };

            return View(model);
        }


        public IActionResult NewsIndex()
        {
            return View();
        }


        public IActionResult PartnerIndex()
        {
            return View();
        }



        /***************Create********************/
        [HttpGet]
        public IActionResult CreateTicket()
        {
            //tests
            var flights = new List<SelectListItem>();
            flights.Add( new SelectListItem 
            {
                    Text = "F192LISOPO121120",
                    Value = "1"
            });
            var partners = new List<SelectListItem>();
            partners.Add(new SelectListItem
            {
                Text = "CinelAir",
                Value = "1"
            });


            var model = new CreateTicketViewModel
            {
                Flights = flights,
                PartnersList = partners
            };

            return PartialView("_CreateTicket", model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketViewModel model)
        {
            //validate fields
            if (ModelState.IsValid)
            {
                //get current user
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
                if (user == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                //add the entry to DB
                var ticket = _converter.ToPremiumTicket(model);
                ticket.CreatedBy = user;
                ticket.CreateDate = DateTime.UtcNow;
                ticket.Status = 1;

                var result = await _premiumRepository.CreateEntryAsync(ticket);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DatacontextError");
                }

                //send notification to SU ? Notification.cs


            }
            return PartialView("_CreateTicket", model);
        }


        [HttpGet]
        public IActionResult CreateUpgrade()
        {
            //tests
            var flights = new List<SelectListItem>();
            flights.Add(new SelectListItem
            {
                Text = "F192LISOPO121120",
                Value = "1"
            });
            var partners = new List<SelectListItem>();
            partners.Add(new SelectListItem
            {
                Text = "CinelAir",
                Value = "1"
            });
            

            var model = new CreateUpgradeViewModel
            {
                Flights = flights,
                PartnersList = partners
            };

            return PartialView("_CreateUpgrade", model);
        }


        [HttpPost]
        public IActionResult CreateUpgrade(CreateUpgradeViewModel model)
        {
            //validate fields
            //get current user
            //add the entry to DB
            //send notification to SU ? Notification.cs

            return RedirectToAction("PremiumIndex");
        }


        public IActionResult CreateVoucher()
        {
            //tests
            var partners = new List<SelectListItem>();
            partners.Add(new SelectListItem
            {
                Text = "CinelAir",
                Value = "1"
            });


            var model = new CreateVoucherViewModel
            {
                PartnersList = partners
            };

            return PartialView("_CreateVoucher", model);
        }


        [HttpPost]
        public IActionResult CreateVoucher(CreateVoucherViewModel model)
        {
            //validate fields
            //get current user
            //add the entry to DB
            //send notification to SU ? Notification.cs

            return RedirectToAction("PremiumIndex");
        }


        /***************EDIT********************/
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //validate id
            if (id == null)
            {
                return new NotFoundViewResult("_404NotFound");//TODO in case of items we could just send a message
            }
            try
            {
                //retrieve entry from DB
                var item = await _premiumRepository.GetByIdAsync(id.Value);
                //check Type of entry
                //switch (item.Type)
                //{
                //    case Enums.PremiumType.Ticket:
                //        break;
                //    case Enums.PremiumType.Upgrade:
                //        break;
                //    case Enums.PremiumType.Voucher:
                //        break;
                //    default:
                //        break;
                //}
                //get view accordingly

                return PartialView("_Edit", item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(PremiumOffer model)//what will enter here??
        {


            return RedirectToAction("PremiumIndex");
        }


        /***************DELETE********************/

        public IActionResult Delete(int? id)
        {
            //delete or status 5 == deleted??
            return RedirectToAction("PremiumIndex");
        }
    }
}
