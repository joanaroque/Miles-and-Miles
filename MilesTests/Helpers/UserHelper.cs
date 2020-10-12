using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public UserHelper(UserManager<User> userManager,
            SignInManager<User> signInManager,
             RoleManager<IdentityRole> roleManager,
             DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = dataContext;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> AddUserWithImageAsync(RegisterNewViewModel model, Guid imageId, string roleName)
        {
            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageId = imageId,
                Email = model.UserName,
                UserName = model.UserName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Document = model.Document,
                City = await _context.Cities.FindAsync(model.CityId),
                RoleId = model.SelectedRole,
                DateOfBirth = model.DateOfBirth
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            User newUser = await GetUserAsync(model.UserName);
            await AddUSerToRoleAsync(newUser, user.RoleId);
            return newUser;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users
                .Include(u => u.City)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IdentityResult> AddUSerToRoleAsync(User user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = _context.Roles.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a role...]",
                Value = "0"
            });

            return list;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
               model.UserName,
               model.Password,
               model.RememberMe,
               false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
               user,
               password,
               false);
        }

        public async Task<User> GetUserImageAsync(Guid userId)
        {
            return await _context.Users
               .Include(u => u.City)
               .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }
    }
}
