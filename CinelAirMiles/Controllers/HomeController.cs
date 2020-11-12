using CinelAirMiles.Helpers;
using CinelAirMiles.Models;
using CinelAirMilesLibrary.Common.Data.Repositories;
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

        public HomeController(
            IAdvertisingRepository advertisingRepository,
            IClientConverterHelper clientConverterHelper,
            IPremiumRepository premiumRepository)
        {
            _advertisingRepository = advertisingRepository;
            _clientConverterHelper = clientConverterHelper;
            _premiumRepository = premiumRepository;
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

                return PartialView("_Feature", modelList);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return RedirectToAction("IndexClient", "Home");
        }

        public async Task<IActionResult> GetPremiumOffer()
        {
            try
            {
                var list = await _premiumRepository.GetPremiumOfferForClientAsync();

                var modelList = new List<PremiumOfferViewModel>(
                    list.Select(a => _clientConverterHelper.ToPremiumOfferViewModel(a))
                    .ToList());

                return PartialView("_Offers", modelList);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return RedirectToAction("IndexClient", "Home");
        }
    }
}
