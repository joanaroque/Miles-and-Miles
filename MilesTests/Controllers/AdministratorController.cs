using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data;
using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Data.Repositories;
using MilesBackOffice.Web.Helpers;
using MilesBackOffice.Web.Models;

namespace MilesBackOffice.Web.Controllers
{
    //todo [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly DataContext _context;

        public AdministratorController(
            IUserHelper userHelper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            ICountryRepository countryRepository,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            DataContext context)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Cities = _countryRepository.GetComboCities(0),
                Roles = _userHelper.GetComboRoles()
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
                        user = new User
                        {
                            Name = model.Name,
                            Email = model.EmailAddress,
                            UserName = model.Username,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            //City = model.CityId,
                            //Country = model.CountryId,
                            SelectedRole = model.SelectedRole,
                            DateOfBirth = model.DateOfBirth,
                            IsActive = model.IsActive
                        };

                        var password = UtilityHelper.Generate();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if (result != IdentityResult.Success)
                        {
                            ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return View(model);
                        }

                        var roleName = await _roleManager.FindByIdAsync(user.SelectedRole.ToString());
                        var register = await _userManager.FindByIdAsync(user.Id);
                        await _userManager.AddToRoleAsync(register, roleName.ToString());
                        ModelState.AddModelError(string.Empty, "User registered with success. Verify email address.");

                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                        var tokenLink = Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        try
                        {
                            _mailHelper.SendMail(model.EmailAddress, "Welcome to the team!", $"To allow the user, " +
                            $"Your username is: {user.UserName}. " +
                            $"Your password is: {password}. Please login and then change it, follow the link:<p><a href = \"{tokenLink}\">Confirm Email</a></p>");

                            //ModelState.Clear();
                            ViewBag.Message = "The instructions to allow your user has been sent to email.";
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "This username is already registered.");
                    model.Countries = _countryRepository.GetComboCountries();
                    model.Cities = _countryRepository.GetComboCities(model.CountryId);
                    model.Roles = _userHelper.GetComboRoles();
                    return View(model);
                }

                return View(model);
            }

            ModelState.AddModelError(string.Empty, "This user already exists.");
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
            var users = await _userManager.Users.ToListAsync();
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

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Username = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                Countries = _countryRepository.GetComboCountries(),
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
        public async Task<IActionResult> EditUser(EditUserViewModel editUser)
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


        //public IActionResult ConfirmUser()
        //{
        //    //var user = _context.Users.Where(u => )
        //}
    }
}