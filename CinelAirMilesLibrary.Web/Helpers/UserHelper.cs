﻿namespace CinelAirMilesLibrary.Common.Helpers
{
    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
            });

            list = list.Where(x => x.Text.Equals("SuperUser") || x.Text.Equals("User") || x.Text.Equals("Admin"));

            return list;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(c => c.Country)
                .Where(u => u.UserName.Equals(username)).FirstOrDefaultAsync();
        }


        public async Task<User> GetUserByGuidIdAsync(string guidId)
        {
            return await _context.Users
                .Include(c => c.Country)
                .Where(u => u.GuidId.Equals(guidId))
                .FirstOrDefaultAsync();
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users
                .Include(c => c.Country)
                .Where(i => i.Id.Equals(userId))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserInRoleAsync(User user, UserType roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName.ToString());
        }

        public async Task<SignInResult> LoginAsync(string username, LoginViewModel model)
        {
            //if(await IsUserClient(username))
            //{
            //    throw new Exception("Incorrect UserName/Password");
            //}

            return await _signInManager.PasswordSignInAsync(
               username,
               model.Password,
               model.RememberMe,
               false);
        }

        private protected async Task<bool> IsUserClient(string username)
        {
            var user = await GetUserByUsernameAsync(username);

            return user.SelectedRole == UserType.Client;
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

        public async Task<Response> DeleteUserAsync(User user)
        {
            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new Response
                {
                    Success = true,
                    Message = "This account has been deleted"
                };
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "An error ocurred while this account was being deleted"
                };
            }
        }


        public async Task<IEnumerable<User>> GetUsersInListAsync(IEnumerable<Notification> list)
        {
            return await _context.Users
                .Include(c => c.Country)
                .Where(u => list.Any(x => x.ItemId == u.GuidId)).ToListAsync();
        }
    }
}
