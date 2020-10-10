﻿using Microsoft.AspNetCore.Identity;
using MilesTests.Data.Entities;
using System;
using System.Threading.Tasks;

namespace MilesTests.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly UserManager<User> _userManager;

        public SeedDB(DataContext context,
            IUserHelper userHelper,
             UserManager<User> userManager)
        {
            _context = context;
            _userHelper = userHelper;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            await CheckOrCreateRoles();

            await FillUser1Async();
            await FillUser2Async();
            await FillUser3Async();






        }

        private async Task FillUser3Async()
        {
            var user = await _userHelper.GetUserByEmailAsync("Joao.Oliveira.Felix@formandos.cinel.pt");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "João",
                    LastName = "Felix",
                    Email = "Joao.Oliveira.Felix@formandos.cinel.pt",
                    UserName = "Joao.Oliveira.Felix@formandos.cinel.pt",
                    PhoneNumber = "965201474",
                    Address = "Rua do Ouro",
                    EmailConfirmed = true

                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUSerToRoleAsync(user, "Admin");
            }
            await _context.SaveChangesAsync();
        }

        private async Task FillUser2Async()
        {
            var user = await _userHelper.GetUserByEmailAsync("catia-96@hotmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Cátia",
                    LastName = "Oliveira",
                    Email = "catia-96@hotmail.com",
                    UserName = "catia-96@hotmail.com",
                    PhoneNumber = "102547455",
                    Address = "Rua da Luz",
                    EmailConfirmed = true

                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUSerToRoleAsync(user, "Admin");
            }

            await _context.SaveChangesAsync();
        }

        private async Task FillUser1Async()
        {
            var user = await _userHelper.GetUserByEmailAsync("joanatpsi@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Joana",
                    LastName = "Roque",
                    Email = "joanatpsi@gmail.com",
                    UserName = "joanatpsi@gmail.com",
                    PhoneNumber = "965214744",
                    Address = "Rua da Programação",
                    EmailConfirmed = true

                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUSerToRoleAsync(user, "Admin");
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckOrCreateRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("User");
            await _userHelper.CheckRoleAsync("SuperUser");
        }
    }
}
