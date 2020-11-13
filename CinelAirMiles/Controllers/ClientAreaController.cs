using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinelAirMiles.Models;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CinelAirMiles.Controllers
{
    public class ClientAreaController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;

        public ClientAreaController(IUserHelper userHelper,
            ICountryRepository countryRepository)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
        }


        public IActionResult AccountManager()
        {
            return View();
        }


        #region USER UPDATE & PASSWORD UPDATE
        public async Task<IActionResult> UpdateClientInfo()
        {
            var user = await _userHelper.GetUserByUsernameAsync(this.User.Identity.Name);

            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.Name = user.Name;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.City = user.City;
                model.TIN = user.TIN;
                model.Name = user.Name;
                model.PhoneNumber = user.PhoneNumber;
            }

            model.Countries = _countryRepository.GetComboCountries();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateClientInfo(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByUsernameAsync(this.User.Identity.Name);
                    if (user != null)
                    {
                        var country = await _countryRepository.GetByIdAsync(model.CountryId);

                        user.Name = model.Name;
                        user.Address = model.Address;
                        user.PhoneNumber = model.PhoneNumber;
                        user.City = model.City;
                        user.Name = model.Name;
                        user.TIN = model.TIN;
                        user.Name = model.Name;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Country = country;

                        var respose = await _userHelper.UpdateUserAsync(user);
                        if (respose.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "User updated successfully!");
                            return View(model);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            else
            {
                return new NotFoundResult();
            }

            return View(model);
        }


        public IActionResult UpdatePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByUsernameAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUserClient");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> DigitalCard()
        {
            try
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                if (user == null)
                {
                    //return new NotFoundViewResult("_Error404Client");
                }

                var model = new DigitalCardViewModel
                {
                    Name = user.Name,
                    ClientNumber = user.GuidId,
                    TierType = user.Tier,
                    ExpirationDate = DateTime.Now.AddYears(1)
                };

                return PartialView("_DigitalCard", model);
            }
            catch (Exception)
            {
                //return new NotFoundViewResult("_Error500Client");
                return RedirectToAction(nameof(AccountManager));
            }

        }
    }
}
