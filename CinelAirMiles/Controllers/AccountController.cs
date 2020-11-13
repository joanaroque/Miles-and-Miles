namespace CinelAirMiles.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using CinelAirMiles.Helpers;

    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using CinelAirMilesLibrary.Common.Models;

    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using MilesBackOffice.Web.Helpers;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;


    public class AccountController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly IClientRepository _clientRepository;
        private readonly IClientConverterHelper _converterHelper;

        public AccountController(
            ICountryRepository countryRepository,
            IUserHelper userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper,
            IClientRepository clientRepository,
            IClientConverterHelper converterHelper)
        {
            _countryRepository = countryRepository;
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _clientRepository = clientRepository;
            _converterHelper = converterHelper;
        }

        public IActionResult AccountManager()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginClient()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginClient(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userHelper.GetUserByGuidId(model.GuidId);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect UserName or Password");
                    return View(model);
                }

                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "You must validate your email before logging in!");
                    return View(model);
                }

                if (user.IsActive == false)
                {
                    ModelState.AddModelError(string.Empty, "Your account is inactive.");
                    return View(model);
                }

                if (user.IsApproved == false)
                {
                    ModelState.AddModelError(string.Empty, "Your account hasn't been approved yet.");
                    return View(model);
                }

                var result = await _userHelper.LoginAsync(user.UserName, model);

                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Indexclient", "Home");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }
            }

            return RedirectToAction("IndexClient", "Home");
        }
        #endregion

        public async Task<IActionResult> LogoutClient()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("IndexClient", "Home");
        }

        #region REGISTER
        public IActionResult RegisterClient()
        {
            var model = new RegisterNewUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Genders = _clientRepository.GetComboGenders()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterClient(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.EmailAddress);
                if (user == null)
                {
                    try
                    {
                        var country = await _countryRepository.GetByIdAsync(model.CountryId);

                        user = _converterHelper.ToUser(model, country);

                        var result = await _userHelper.AddUserAsync(user, model.Password);

                        if (result != IdentityResult.Success)
                        {
                            this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return this.View(model);
                        }

                        await _userHelper.AddUSerToRoleAsync(user, user.SelectedRole);

                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                        var tokenLink = this.Url.Action("ConfirmEmailClient", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.EmailAddress, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                            $"Confirm this is your email by clicking the followiing link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>" +
                            $"</br></br></br>Your account is waiting approval. " +
                            $"We'll let you know when it's approved and ready for you to use it.");

                        ModelState.AddModelError(string.Empty, "Verify your email.");


                        return this.View(model);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                this.ModelState.AddModelError(string.Empty, "There's already an account with this email address.");
            }

            return View(model);
        }



        public async Task<IActionResult> ConfirmEmailClient(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            return View();
        }
        #endregion


        #region ACCOUNT RECOVER
        public IActionResult ClientRecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClientRecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userHelper.GetUserByGuidId(model.GuidId);

                var email = user.Email;

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This Id doesn't correspont to a registered user.");
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = Url.Action(
                    "ResetPasswordClient",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                try
                {
                    _mailHelper.SendMail(email, "Password Reset", $"<h1>Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");

                    ModelState.AddModelError(string.Empty, "The instructions to recover your password have been sent to email.");

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);

                }


                return View();

            }

            return View(model);
        }


        public IActionResult ResetPasswordClient(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPasswordClient(ResetPasswordViewModel model)
        {
            var user = _userHelper.GetUserByGuidId(model.GuidId);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successfully.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }
        #endregion

        public IActionResult NotAuthorizedClient()
        {
            return View();
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


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
        //        new { ReturnUrl = returnUrl });

        //    var properties =
        //        _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));

        //    return new ChallengeResult(provider, properties);
        //}

        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    LoginViewModel loginViewModel = new LoginViewModel
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalLogins =
        //        (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };

        //    if (remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty,
        //            $"Error from external provider: {remoteError}");

        //        return View("LoginClient", loginViewModel);
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();

        //    if (info == null)
        //    {
        //        return View("LoginClient", loginViewModel);
        //    }

        //    var signResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
        //        info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        //    if (signResult.Succeeded)
        //    {
        //        return LocalRedirect(returnUrl);
        //    }

        //    else if (signResult.IsLockedOut)
        //    {
        //        return RedirectToAction(nameof(ClientRecoverPassword));
        //    }

        //    else
        //    {
        //        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        if (email != null)
        //        {
        //            var user = await _userHelper.GetUserByEmailAsync(email);
        //            if (user == null)
        //            {
        //                user = new User
        //                {
        //                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
        //                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        //                };

        //                await _userManager.CreateAsync(user);
        //            }

        [HttpGet]
        public async Task<IActionResult> DigitalCard()    
        {
            try
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

        //            return LocalRedirect(returnUrl);
        //        }

        //        ViewBag.ErrorTittle = $"Error claim not received from: {info.LoginProvider}";

        //        return View("Error");
        //    }
        //}


    }
}