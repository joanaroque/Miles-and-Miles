namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.Admin;

    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IClientRepository _clientRepository;

        public AdministratorController(
            IUserHelper userHelper,
            ICountryRepository countryRepository,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            IClientRepository clientRepository)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _clientRepository = clientRepository;
        }

        
        public IActionResult NewClients()
        {
            var usersList = _clientRepository.GetNewClients();

            var list = usersList.Select(u => _converterHelper.ToUserViewModel(u));

            return View(list);
        }


        public async Task<IActionResult> ApproveClient(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            try
            {
                var user = await _userHelper.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
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
            catch (DBConcurrencyException)
            {
                return NotFound();
            }
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
                            EmailConfirmed = true
                        };

                        var password = UtilityHelper.Generate();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return View(model);
                        }

                        await _userHelper.AddUSerToRoleAsync(user, user.SelectedRole);

                        var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                        var link = Url.Action(
                            "ResetPassword",
                            "Account",
                            new
                            {
                                token = myToken,
                                userId = user.Id
                            },
                            protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendToNewUser(user.Email, link, user.Name);

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
        public ActionResult ListUsers()
        {
            var users = _clientRepository.GetActiveUsers();
            var model = new List<UserDetailsViewModel>();

            foreach (User user in users)
            {
                var viewModel = new UserDetailsViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Email,
                    SelectedRole = user.SelectedRole
                };
                model.Add(viewModel);
            }

            return View(model);
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
                SelectedRole = user.SelectedRole
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
            try
            {
                var user = await _userHelper.GetUserByIdAsync(id);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                var result = await _userHelper.DeleteUserAsync(user);

                if (!result.Success)
                {
                    return NotFound();//TODO refactor
                }

                _mailHelper.SendMail(user.Email, "CinelAir Miles confirmation", result.Message);//TODO refactor

                return RedirectToAction("ListUsers");
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_404Error");
            }
        }



        public IActionResult UserNotFound()
        {
            return new NotFoundViewResult("UserNotFound");
        }
    }
}