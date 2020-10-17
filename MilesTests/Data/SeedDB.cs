using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data.Entities;

namespace MilesBackOffice.Web.Data
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

            await FillCountriesAsync();

            await FillUser1Async();
            await FillUser2Async();
            await FillUser3Async();

        }

        private async Task FillUser3Async()
        {
            var user = await _userHelper.GetUserByEmailAsync("jpofelix@gmail.com");

            if (user == null)
            {

                user = new User
                {
                    Name = "João Felix",
                    Email = "jpofelix@gmail.com",
                    UserName = "JoaoFelix",
                    PhoneNumber = "965201474",
                    Address = "Rua do Ouro",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/10/1983"),
                    Gender = "Male",
                    City = await _context.Cities.Where(c => c.Name == "Lisboa").FirstOrDefaultAsync(),
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121218",
                    Document = "20174255"

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
                    Name = "Cátia Oliveira",
                    Email = "catia-96@hotmail.com",
                    UserName = "CatiaOliveira",
                    PhoneNumber = "102547455",
                    Address = "Rua da Luz",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/09/1997"),
                    Gender = "Female",
                    City = await _context.Cities.Where(c => c.Name == "Lisboa").FirstOrDefaultAsync(),
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121217",
                    Document = "2014742955"

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
                    Name = "Joana Roque",
                    Email = "joanatpsi@gmail.com",
                    UserName = "JoanaRoque",
                    PhoneNumber = "965214744",
                    Address = "Rua da Programação",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("27/11/1988"),
                    Gender = "Female",
                    City = await _context.Cities.Where(c => c.Name == "Lisboa").FirstOrDefaultAsync(),
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121212",
                    Document = "201474255"
                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "SuperUser");

            if (!isInRole)
            {
                var identityResult = await _userHelper.AddUSerToRoleAsync(user, "SuperUser");
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckOrCreateRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("User");
            await _userHelper.CheckRoleAsync("SuperUser");
        }

        private async Task FillCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var cities = new List<City>
                {
                    new City { Name = "Lisboa" },
                    new City { Name = "Porto" },
                    new City { Name = "Coimbra" },
                    new City { Name = "Faro" }
                };


                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });


                await _context.SaveChangesAsync();
            }
        }
    }
}