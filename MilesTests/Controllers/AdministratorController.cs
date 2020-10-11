using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data;
using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;
using MilesBackOffice.Web.Models;

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

        public AdministratorController(IUserHelper userHelper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        //public IActionResult ListUsers()
        //{
        //    var users = _userManager.Users.ToList();

        //    return View(users);
        //}
        public async Task<ActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRoleViewModel>();

            foreach (User user in users)
            {
                var thisViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Name = user.FullName,
                    UserName = user.Email,
                    Roles = await GetUserRoles(user)
                };
                userRolesViewModel.Add(thisViewModel);
            }

            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


        // GET: Administrator/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditUser(string id) // todo passar o role
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
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
        /// Remove roles already associated with the user, which was not selected
        /// If the role has not selected and is in use, I remove the role
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

                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.Address = editUser.Address;
                user.PhoneNumber = editUser.PhoneNumber;

                var selectedRole = await _roleManager.FindByIdAsync(editUser.SelectedRole);

                foreach (var currentRole in _roleManager.Roles.ToList())
                {
                    var isSelectedRole = selectedRole.Name.Equals(currentRole.Name);
                    if (!isSelectedRole && await _userHelper.IsUserInRoleAsync(user, currentRole.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(user, currentRole.Name);
                    }
                }

                await _userHelper.AddUSerToRoleAsync(user, selectedRole.Name);

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
    }
}