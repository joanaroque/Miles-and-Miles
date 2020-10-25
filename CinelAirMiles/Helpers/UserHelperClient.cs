namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;

    using global::CinelAirMiles.Data;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserHelperClient : IUserHelperClient
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContextClients _context;


        public UserHelperClient(UserManager<User> userManager,
            SignInManager<User> signInManager,
             RoleManager<IdentityRole> roleManager,
             DataContextClients dataContext)
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


        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IdentityResult> AddUSerToRoleAsync(User user, UserType roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName.ToString());
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


        public async Task<IdentityRole> FindRoleByTypeAsync(UserType role)
        {
            return await _roleManager.FindByNameAsync(role.ToString());
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
            var list = Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();


            return list;
        }



        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> IsUserInRoleAsync(User user, UserType roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName.ToString());
        }

        public async Task<SignInResult> LoginAsync(ClientLoginViewModel model)
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


        public async Task RemoveRoleAsync(User user, UserType type)
        {
            await _userManager.RemoveFromRoleAsync(user, type.ToString());
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
                              .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }















    }

}

