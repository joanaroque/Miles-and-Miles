namespace CinelAirMiles.Controllers
{
    using CinelAirMiles.Helpers;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Helpers;
    using CinelAirMilesLibrary.Common.Models;
    using CinelAirMilesLibrary.Common.Web.Helpers;
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
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IClientRepository _clientRepository;
        private readonly IClientConverterHelper _converterHelper;

        public AccountController(
            ICountryRepository countryRepository,
            IUserHelper userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IClientRepository clientRepository,
            IClientConverterHelper converterHelper)
        {
            _countryRepository = countryRepository;
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _signInManager = signInManager;
            _userManager = userManager;
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
        public async Task<IActionResult> LoginClient(LoginViewModel model, string returnUrl)
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
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
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




        public async Task<IActionResult> LogoutClient()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("IndexClient", "Home");
        }


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

        public IActionResult ChangePasswordClient()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ChangePasswordClient(ChangePasswordViewModel model)
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


        public IActionResult NotAuthorizedClient()
        {
            return View();
        }

        public async Task<IActionResult> ChangeUserClient()
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
        public async Task<IActionResult> ChangeUserClient(ChangeUserViewModel model)
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
                return new NotFoundViewResult("_Error404Client");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> DigitalCard()    
        {
            try
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                if (user == null)
                {
                    return new NotFoundViewResult("_Error404Client");
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
                return new NotFoundViewResult("_Error500Client");
            }

        }

    }
}