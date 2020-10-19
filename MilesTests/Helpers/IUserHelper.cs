﻿namespace MilesBackOffice.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Enums;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.Admin;

    public interface IUserHelper
    {

        Task<User> GetUserByUsernameAsync(string username);


        /// <summary>
        /// find user by email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>user</returns>
        Task<User> GetUserByEmailAsync(string email);


        /// <summary>
        /// create a user
        /// </summary>
        /// <param name="user">iser</param>
        /// <param name="password">password</param>
        /// <returns>created user</returns>
        Task<IdentityResult> AddUserAsync(User user, string password);


        /// <summary>
        /// login with email password and save this information
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>email password with saved information</returns>
        Task<SignInResult> LoginAsync(LoginViewModel model);


        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();



        /// <summary>
        /// update the user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>updated user</returns>
        Task<IdentityResult> UpdateUserAsync(User user);



        /// <summary>
        /// change the user password
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="oldPassword">oldPassword</param>
        /// <param name="newPassword">newPassword</param>
        /// <returns>password changed</returns>
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);


        /// <summary>
        /// check if the user is right with the input password
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        Task<SignInResult> ValidatePasswordAsync(User user, string password);



        /// <summary>
        /// check if the role exists, if not, create a new one
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <returns>a new role if it doesn't exist</returns>
        Task CheckRoleAsync(string roleName);


        /// <summary>
        /// check if the user is in the role
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="roleName">role Name</param>
        /// <returns>if the user is in the role</returns>
        Task<bool> IsUserInRoleAsync(User user, UserType roleName);



        /// <summary>
        /// assigns the role to the user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="roleName">role Name</param>
        /// <returns>user in role</returns>
        Task<IdentityResult> AddUSerToRoleAsync(User user, UserType roleName);



        /// <summary>
        /// generates a token confirmation email for the user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns> email for the user</returns>
        Task<string> GenerateEmailConfirmationTokenAsync(User user);


        /// <summary>
        /// validates whether the confirmation email matches the specific user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="token">token</param>
        /// <returns>confirmation email with the specific user</returns>
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);


        /// <summary>
        /// find user by id
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>user</returns>
        Task<User> GetUserByIdAsync(string userId);


        /// <summary>
        /// generate a password
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>user password</returns>
        Task<string> GeneratePasswordResetTokenAsync(User user);



        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="token">token</param>
        /// <param name="password">password</param>
        /// <returns>user new password</returns>
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);




        IEnumerable<SelectListItem> GetComboRoles();



        Task<User> AddUserWithImageAsync(RegisterUserViewModel model, Guid imageId, string roleName);



        Task<User> GetUserImageAsync(Guid userId);




        Task<IdentityRole> FindRoleByTypeAsync(UserType role);







        Task RemoveRoleAsync(User user, UserType type);
    }
}
