namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Enums;
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


        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUser();

                RedirectUserForRole(user);
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
                        throw new Exception("Username and Password Incorrect");
                    }

                    var result = await _userHelper.LoginAsync(model.UserName, model);
                    if (result.Succeeded)
                    {
                        return RedirectUserForRole(user);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username or Password Incorrect.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }


        private protected IActionResult RedirectUserForRole(User user)
        {
            if (user.SelectedRole == UserType.Admin)
            {
                return RedirectToAction("ListUsers", "Administrator");
            }
            else if (user.SelectedRole == UserType.SuperUser)
            {
                return RedirectToAction("PremiumIndex", "SuperUser");
            }
            else if (user.SelectedRole == UserType.User)
            {
                return RedirectToAction("PremiumIndex", "User");
            }
            else
            {
                return View(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction(nameof(AccountController.Login));
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
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return new NotFoundViewResult("_Error404");
            }

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
                try
                {
                    var user = await _userHelper.GetUserByIdAsync(model.UserId);
                    if (user != null)
                    {
                        var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                        if (result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "You can log with your UserName and Password");
                            return RedirectToAction(nameof(Login));
                        }

                        ModelState.AddModelError(string.Empty, "Error while resetting the password.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
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