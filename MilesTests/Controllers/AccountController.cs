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
        private readonly INotificationHelper _notificationHelper;
        private readonly INotificationRepository _notificationRepository;

        public AccountController(IUserHelper userHelper,
              ICountryRepository countryRepository,
              IConverterHelper converterHelper,
              INotificationHelper notificationHelper,
              INotificationRepository notificationRepository)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _converterHelper = converterHelper;
            _notificationHelper = notificationHelper;
            _notificationRepository = notificationRepository;
        }


        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUser();
                if (user == null)
                {
                    return await Logout();
                }
                return RedirectUserForRole(user);
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

                    if (user.IsActive == false)
                    {
                        throw new Exception("Your account is inactive.");
                    }

                    if (user.SelectedRole == UserType.Client)
                    {
                        throw new Exception("Username or Password Invalid.");
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
        public IActionResult RecoverAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverAccount(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user != null && user.SelectedRole != UserType.Client)
                {
                    await _notificationHelper.CreateNotificationAsync(user.GuidId, UserType.Admin, "Send Reset Password Email.", NotificationType.Recover);

                    ModelState.AddModelError(string.Empty, "Notification sent to admin.");
                    return View();
                }
                ModelState.AddModelError(string.Empty, "There is no user registered with that email.");
            }
            return View(model);
        }
        #endregion



        public IActionResult ResetPassword(string userId, string validToken)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(validToken))
            {
                return new NotFoundViewResult("_Error404");
            }

            var model = new ResetPasswordViewModel
            {
                UserId = userId,
                Token = validToken
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
                    var user = await _userHelper.GetUserByGuidIdAsync(model.UserId);
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
            var user = User.Identity.Name;
            if (user == null)
            {
                return null;
            }
            return await _userHelper.GetUserByUsernameAsync(user);
        }
    }
}