namespace MilesBackOffice.Web.Controllers
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Helpers;
    using CinelAirMilesLibrary.Common.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IBlobHelper _blobHelper;
        private readonly IClientRepository _clientRepository;

        public AccountController(IUserHelper userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper,
            SignInManager<User> signInManager,
              UserManager<User> userManager,
              ICountryRepository countryRepository,
              IBlobHelper blobHelper,
              IClientRepository clientRepository)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _signInManager = signInManager;
            _userManager = userManager;
            _countryRepository = countryRepository;
            _blobHelper = blobHelper;
            _clientRepository = clientRepository;
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
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError(string.Empty, "Failed to login.");
            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        //public IActionResult Register()
        //{
        //    var model = new RegisterNewUserViewModel
        //    {
        //        Countries = _countryRepository.GetComboCountries(),
        //        Cities = _countryRepository.GetComboCities(0),
        //        Genders = _clientRepository.GetComboGenders()
        //    };

        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userHelper.GetUserByEmailAsync(model.Username);
        //        if (user == null)
        //        {
        //            var city = await _countryRepository.GetCityAsync(model.CityId);

        //            user = new User
        //            {
        //                Name = model.Name,
        //                Email = model.Username,
        //                UserName = model.Username,
        //                Address = model.Address,
        //                PhoneNumber = model.PhoneNumber,
        //                City = city,
        //                IsActive = false,
        //                IsApproved = false,
        //                BonusMiles = 0,
        //                DateOfBirth = model.DateOfBirth,
        //                Gender = model.Gender.ToString(),
        //                GuidId = _clientRepository.CreateGuid(),
        //                TIN = model.TIN,
        //                Status = TierType.Miles,
        //                StatusMiles = 0
        //            };

        //            var result = await _userHelper.AddUserAsync(user, model.Password);
        //            if (result != IdentityResult.Success)
        //            {
        //                ModelState.AddModelError(string.Empty, "The user couldn't be created.");
        //                return this.View(model);
        //            }

        //            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        //            var tokenLink = Url.Action("ConfirmEmail", "Account", new
        //            {
        //                userid = user.Id,
        //                token = myToken
        //            }, protocol: HttpContext.Request.Scheme);

        //            _mailHelper.SendMail(model.EmailAddress, "Email confirmation", $"<h1>Email Confirmation</h1>" +
        //                $"To allow the user, " +
        //                $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
        //            ViewBag.Message = "The instructions to allow your user has been sent to email.";


        //            return View(model);
        //        }

        //        ModelState.AddModelError(string.Empty, "The user already exists.");

        //    }

        //    return View(model);
        //}


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);
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
                            expires: DateTime.UtcNow.AddMonths(4),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
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

        public IActionResult ResetPassword(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Password reset successful.";
                    return View();
                }

                ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            ViewBag.Message = "User not found.";
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}