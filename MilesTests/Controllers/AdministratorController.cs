using CinelAirMilesLibrary.Common.Data;
using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Enums;
using CinelAirMilesLibrary.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MilesBackOffice.Web.Helpers;
using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.Admin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly DataContext _context;
        private readonly IClientRepository _clientRepository;

        public AdministratorController(
            IUserHelper userHelper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            ICountryRepository countryRepository,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            DataContext context,
            IClientRepository clientRepository)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _context = context;
            _clientRepository = clientRepository;
        }

        public IActionResult InactiveUsers()
        {
            var users = _clientRepository.GetInactiveUsers();
            var model = new List<InactiveUsersViewModel>();

            foreach (User user in users)
            {
                var viewModel = new InactiveUsersViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    TIN = user.TIN,
                    SelectedRole = user.SelectedRole,
                };
                model.Add(viewModel);
            }

            return View(model);
        }

        public IActionResult NewClients()
        {
            var users = _clientRepository.GetNewClients();
            var model = new List<NewClientsViewModel>();

            foreach (User user in users)
            {
                var viewModel = new NewClientsViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    TIN = user.TIN
                };
                model.Add(viewModel);
            }

            return View(model);
        }


        public async Task<IActionResult> ApproveClient(string id)
        {
            var user = await _userHelper.GetUserByIdAsync(id);

            var model = new ApproveClientViewModel
            {
                Name = user.Name,
                Username = user.UserName,
                Address = user.Address,
                City = user.City,
                CountryId = user.Country.Id,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Status = user.Tier,
                TIN = user.TIN
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClient(ApproveClientViewModel model)
        {
            //TODO !!! enviar email quando o user é aprovado + trycatch

            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.Id);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                user.IsApproved = model.IsApproved;

                var result = await _userHelper.UpdateUserAsync(user);

                if (result.Succeeded)
                {
                    _mailHelper.SendMail(model.Email, "CinelAir Miles confirmation", $"Your account was approved. You can now log in.");
                    return RedirectToAction("NewClients");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Roles = _userHelper.GetComboRoles(),
                Genders = _clientRepository.GetComboGenders()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.EmailAddress);

                if (user == null)
                {
                    try
                    {
                        var country = await _countryRepository.GetByIdAsync(model.CountryId);
                        user = new User
                        {
                            Name = model.Name,
                            Email = model.EmailAddress,
                            UserName = model.Username,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Country = country,
                            City = model.City,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender,
                            TIN = model.TIN,
                            SelectedRole = model.SelectedRole,
                            IsActive = true,
                            IsApproved = true,
                            EmailConfirmed = false
                        };

                        var password = UtilityHelper.Generate();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if (result != IdentityResult.Success)
                        {
                            ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return View(model);
                        }

                        await _userHelper.AddUSerToRoleAsync(user, user.SelectedRole);

                        var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                        var link = Url.Action(
                            "ResetPassword",
                            "Account",
                            new { token = myToken }, protocol: HttpContext.Request.Scheme);


                        _mailHelper.SendMail(user.Email, "Account Created", $"<h1>Complete Account Registration</h1>" +
                        $"Follow the link to complete your registration:</br></br>" +
                        $"<a href = \"{link}\">Complete Registration</a>" +
                        $"/br <p>Your Username: {user.UserName}.</p>" +
                        $"/br");

                        return RedirectToAction(nameof(NewClients));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This username is already registered.");
                }
            }
            model.Countries = _countryRepository.GetComboCountries();
            model.Roles = _userHelper.GetComboRoles();
            model.Genders = _clientRepository.GetComboGenders();
            return View(model);
        }



        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        public async Task<ActionResult> ListUsers()
        {
            var users = _clientRepository.GetActiveUsers();
            var model = new List<UserRoleViewModel>();

            foreach (User user in users)
            {
                var viewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Name = user.Name,
                    UserName = user.Email,
                    Roles = await GetUserRoles(user),
                };
                model.Add(viewModel);
            }

            return View(model);
        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> DetailsUser(string id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var user = await _userHelper.GetUserByIdAsync(id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View(user);
        }

        // GET: Administrator/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userHelper.GetUserByIdAsync(id);

            if (id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new UserDetailsViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Username = user.UserName,
                City = user.City,
                Email = user.Email,
                IsActive = user.IsActive,
                Status = user.Tier,
                StatusMiles = user.StatusMiles,
                BonusMiles = user.BonusMiles,
                Genders = _clientRepository.GetComboGenders(),
                Countries = _countryRepository.GetComboCountries(),
                StatusList = _clientRepository.GetComboStatus(),
                Roles = _roleManager.Roles.ToList().Select(
                    x => new SelectListItem()
                    {
                        Selected = userRoles.Contains(x.Name),
                        Text = x.Name,
                        Value = x.Id
                    })
            };

            return View(model);
        }

        /// <summary>
        /// gets the user by id
        /// assign new properties to current user
        /// Remove roles already associated with the user, which were not selected
        /// If the role was not selected and is in use, I remove the role
        /// Assign new role
        /// Update user after assigning new role
        /// If the update operation is successful, send to the user list page
        /// </summary>
        /// <param name="editUser">model edit user</param>
        /// <returns>updated user</returns>
        // POST: Administrator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserDetailsViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(editUser.Id);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                user.Name = editUser.Name;
                user.Address = editUser.Address;
                user.PhoneNumber = editUser.PhoneNumber;
                user.IsActive = editUser.IsActive;
                user.DateOfBirth = editUser.DateOfBirth;
                user.City = editUser.City;
                user.Country.Id = editUser.CountryId;
                user.Gender = editUser.Gender;
                user.TIN = editUser.TIN;


                await _userHelper.RemoveRoleAsync(user, user.SelectedRole);

                await _userHelper.AddUSerToRoleAsync(user, editUser.SelectedRole);

                var result = await _userHelper.UpdateUserAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(editUser);
        }

        // POST: Administrator/Delete/5
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userHelper.GetUserByIdAsync(id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _mailHelper.SendMail(user.Email, "CinelAir Miles confirmation", $"Your account was deleted.");
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("ListRoles");

        }

        public IActionResult UserNotFound()
        {
            return new NotFoundViewResult("UserNotFound");
        }


        //public IActionResult ConfirmClientDelete() TODO
        //{
        //    var user = _context.Users.Where(u => )
        //}
    }
}