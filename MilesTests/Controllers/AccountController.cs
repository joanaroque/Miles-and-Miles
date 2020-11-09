namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using CinelAirMilesLibrary.Common.Models;

    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;

    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IConverterHelper _converterHelper;

        public AccountController(IUserHelper userHelper,
              ICountryRepository countryRepository,
              IConverterHelper converterHelper)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _converterHelper = converterHelper;
        }


        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByUsernameAsync(model.UserName);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    var result = await _userHelper.LoginAsync(model.UserName, model);
                    if (result.Succeeded)
                    {
                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            return Redirect(Request.Query["ReturnUrl"].First());
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to login.");

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AccountDetails()
        {
            var user = await GetCurrentUser();

            return View(_converterHelper.ToUserViewModel(user));
        }

        
        #region Recover Password
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    //Enviar notificação ao Admin do pedido do user
                    //TODO enviar msg de OK
                    RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
            }

            return View(model);
        }
        #endregion //TODO refactor



        public IActionResult ResetPassword(string userId, string token)
        {
            var model = new ResetPasswordViewModel
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        await _userHelper.UpdateUserAsync(user);

                        return RedirectToAction(nameof(Login));
                    }

                    ModelState.AddModelError(string.Empty, "Error while resetting the password.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }


        private async Task<User> GetCurrentUser()
        {
            return await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
        }
    }
}