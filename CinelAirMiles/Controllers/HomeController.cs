using CinelAirMiles.Helpers;
using CinelAirMiles.Models;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Helpers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinelAirMiles.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IClientConverterHelper _clientConverterHelper;
        private readonly IPremiumRepository _premiumRepository;
        private readonly IMailHelper _mailHelper;

        public HomeController(
            IAdvertisingRepository advertisingRepository,
            IClientConverterHelper clientConverterHelper,
            IPremiumRepository premiumRepository,
            IMailHelper mailHelper)
        {
            _advertisingRepository = advertisingRepository;
            _clientConverterHelper = clientConverterHelper;
            _premiumRepository = premiumRepository;
            _mailHelper = mailHelper;
        }

        public IActionResult IndexClient()
        {
            return View();
        }


        public async Task<IActionResult> GetAdvertising()
        {
            try
            {
                var list = await _advertisingRepository.GetAdvertisingForClientAsync();

                var modelList = new List<AdvertisingViewModel>(
                    list.Select(a => _clientConverterHelper.ToAdvertisingViewModel(a))
                    .ToList());

                return PartialView("_Feature", modelList.Take(4));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return RedirectToAction("IndexClient", "Home");
        }

        public FileResult GetFileFromBytes(byte[] bytesIn)
        {
            return File(bytesIn, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisingImageFile(int id)
        {
            var advertising = await _advertisingRepository.GetByIdWithIncludesAsync(id);

            FileResult imageUserFile = GetFileFromBytes(advertising.Image);
            return imageUserFile;
        }

        public async Task<IActionResult> GetPremiumOffer()
        {
            try
            {
                var list = await _premiumRepository.GetAllIncludes();

                var modelList = list.Select(a => _clientConverterHelper.ToPremiumOfferViewModel(a));

                return PartialView("_Offers", modelList.Take(4));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView("_Error404Client");
            }
        }

        [HttpPost]
        public IActionResult SubscriveNewsletter(string email)
        {
            string message;
            var result = _mailHelper.SendNewsletterConfirmation(email);
            if (!result)
            {
                message = "Something went wrong!";
            }
            else
            {
                message = "Thank you for subscriving.";
            }
            
            return Json(message);
        }
        [HttpGet]
        public async Task<IActionResult> GetOffersImageFile(int id)
        {
            var offer = await _premiumRepository.GetByIdWithIncludesAsync(id);

            FileResult imageUserFile = GetFileFromBytes(offer.Image);
            return imageUserFile;
        }
    }
}
