﻿namespace CinelAirMiles.Controllers
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using global::CinelAirMiles.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;


    public class AccountController : Controller
    {
        //todo private readonly ICountryRepository _countryRepository;
        private readonly IUserHelperClient _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            //ICountryRepository countryRepository,
            IUserHelperClient userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            //_countryRepository = countryRepository;
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _signInManager = signInManager;
            _userManager = userManager;
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginClient(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginClient(LoginViewModel model, string returnUrl)
        {
            model.ExternalLogins =
                (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {

                var user = await _userHelper.GetUserByUsernameAsync(model.UserName);

                if (user != null && !user.EmailConfirmed &&
                             (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            //TODO redirect to logged User Home
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                new { ReturnUrl = returnUrl });

            var properties =
                _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty,
                    $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return View("Login", loginViewModel);
            }

            var signResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            else if (signResult.IsLockedOut)
            {
                return RedirectToAction(nameof(RecoverPassword));
            }

            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await _userHelper.GetUserByEmailAsync(email);
                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await _userManager.CreateAsync(user);
                    }

                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTittle = $"Error claim not received from: {info.LoginProvider}";

                return View("Error");
            }
        }


        public async Task<IActionResult> LogoutClient()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult RegisterClient()
        {
            var model = new RegisterNewUserViewModel
            {
                //Countries = _countryRepository.GetComboCountries(),
                //Cities = _countryRepository.GetComboCities(0),
                //Genders = TODO passar repos para common???
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterClient(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {

                    //todo var city = await _countryRepository.GetCityAsync(model.CityId);


                    user = new User
                    {
                        Name = model.Name,
                        Email = model.Username,
                        UserName = model.Username,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        //City = city
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";


                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists.");

            }

            return View(model);
        }



        public async Task<IActionResult> ConfirmEmailClient(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            return View();
        }

        public IActionResult ChangePasswordClient()

        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ChangePasswordClient(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
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
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                try
                {
                    _mailHelper.SendMail(model.Email, "Password Reset", $"<h1>Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");

                    //ModelState.Clear();
                    ViewBag.Message = "The instructions to recover your password has been sent to email.";

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);

                }


                return View();

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenClient([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                             _configuration["Tokens:Issuer"],
                             _configuration["Tokens:Audience"],
                             claims,
                             expires: DateTime.UtcNow.AddDays(15),
                             signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }

        public IActionResult RecoverPasswordClient()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> RecoverPasswordClient(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "Shop Password Reset", $"<h1>Shop Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");
                this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return this.View();

            }

            return this.View(model);
        }

        public IActionResult ResetPasswordClient(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPasswordClient(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }

        //public async Task<JsonResult> GetCitiesAsync(int countryId)
        //{
        //    //var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
        //    return this.Json(country.Cities.OrderBy(c => c.Name));
        //}

        public async Task<IActionResult> ChangeUserClient()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.Name = user.Name;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;

                //todo var city = await _countryRepository.GetCityAsync(user.CityId);
                //if (city != null)
                //{
                //    var country = await _countryRepository.GetCountryAsync(city);
                //    if (country != null)
                //    {
                //        model.CountryId = country.Id;
                //        model.Cities = _countryRepository.GetComboCities(country.Id);
                //        model.Countries = _countryRepository.GetComboCountries();
                //        model.CityId = user.CityId;
                //    }
                //}
            }

            //model.Cities = _countryRepository.GetComboCities(model.CountryId);
            //model.Countries = _countryRepository.GetComboCountries();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ChangeUserClient(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    //var city = await _countryRepository.GetCityAsync(model.CityId);

                    user.Name = model.Name;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.City.Id = model.CityId;
                    //user.City = city;

                    var respose = await _userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }
    }
}