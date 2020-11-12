﻿using CinelAirMiles.Helpers;
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

        public HomeController(
            IAdvertisingRepository advertisingRepository,
            IClientConverterHelper clientConverterHelper)
        {
            _advertisingRepository = advertisingRepository;
            _clientConverterHelper = clientConverterHelper;
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
