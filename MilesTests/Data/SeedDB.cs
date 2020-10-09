using Microsoft.AspNetCore.Identity;
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

            await FillUserAsync();







        }

        private async Task FillUserAsync()
        {
            var user1 = await _userHelper.GetUserByEmailAsync("joanatpsi@gmail.com");

            if (user1 == null)
            {
                user1 = new User
                {
                    FirstName = "Joana",
                    LastName = "Roque",
                    Email = "joanatpsi@gmail.com",
                    UserName = "joanatpsi@gmail.com",
                    PhoneNumber = "965214744",
                    Address = "Rua da Programação",
                    EmailConfirmed = true

                };

                await _userHelper.AddUserAsync(user1, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user1);
                await _userHelper.ConfirmEmailAsync(user1, token);
            }

            var isInRole1 = await _userHelper.IsUserInRoleAsync(user1, "Admin");

            if (!isInRole1)
            {
                await _userHelper.AddUSerToRoleAsync(user1, "Admin");
            }

            var user2 = await _userHelper.GetUserByEmailAsync("catia-96@hotmail.com");

            if (user2 == null)
            {
                user2 = new User
                {
                    FirstName = "Cátia",
                    LastName = "Oliveira",
                    Email = "catia-96@hotmail.com",
                    UserName = "catia-96@hotmail.com",
                    PhoneNumber = "102547455",
                    Address = "Rua da Luz",
                    EmailConfirmed = true

                };

                await _userHelper.AddUserAsync(user2, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user2);
                await _userHelper.ConfirmEmailAsync(user2, token);
            }

            var isInRole2 = await _userHelper.IsUserInRoleAsync(user2, "Admin");

            if (!isInRole2)
            {
                await _userHelper.AddUSerToRoleAsync(user2, "Admin");
            }

            var user3 = await _userHelper.GetUserByEmailAsync("Joao.Oliveira.Felix@formandos.cinel.pt");

            if (user3 == null)
            {
                user3 = new User
                {
                    FirstName = "João",
                    LastName = "Felix",
                    Email = "Joao.Oliveira.Felix@formandos.cinel.pt",
                    UserName = "Joao.Oliveira.Felix@formandos.cinel.pt",
                    PhoneNumber = "965201474",
                    Address = "Rua do Ouro",
                    EmailConfirmed = true

                };

                await _userHelper.AddUserAsync(user2, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user3);
                await _userHelper.ConfirmEmailAsync(user3, token);
            }

            var isInRole3 = await _userHelper.IsUserInRoleAsync(user3, "Admin");

            if (!isInRole3)
            {
                await _userHelper.AddUSerToRoleAsync(user3, "Admin");
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
