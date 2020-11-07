using CinelAirMilesLibrary.Common.Data;
using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Enums;
using CinelAirMilesLibrary.Common.Helpers;
using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Helpers;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;

        public SeedDB(DataContext context,
            IUserHelper userHelper,
            IClientRepository clientRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _clientRepository = clientRepository;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckOrCreateRoles();

            await FillCountriesAsync();

            await FillUser1Async();
            await FillUser2Async();
            await FillUser3Async();

            // Clients
            await FillUser4Async();
            await FillUser5Async();
            await FillUser6Async();

            await AddTierChanges();
            await AddClientComplaint();

            //User FakeDB
            await AddFlights();

            await AddPartnership();
            await AddOffers();

            await AddAdvertising();

            //client project !!
           // await AddReservations();
            await AddNotifications();

        }

        private async Task AddNotifications()
        {
            if (!_context.Notifications.Any())
            {
                var partner = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("jacintoafonso@yopmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 8,
                    Message = "bla bla bla notifications bla bla bla notifications!!!! "
                });

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 8,
                    Message = "bla bla bla notifications bla bla bla notifications " +
                    "bla bla bla notifications!!!! "
                });

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 8,
                    Message = "bla bla bla notifications bla bla bla notifications" +
                    "bla bla bla notifications" +
                    "bla bla bla notifications!!!! "
                });

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 8,
                    Message = "bla bla bla notifications bla bla bla notifications" +
                    "bla bla bla notifications" +
                    "bla bla bla notifications" +
                    "bla bla bla notifications!!!! "
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddReservations()
        {
            if (!_context.Reservations.Any())
            {
                var partner = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();
                var departure = "Lisbon";

                _context.Reservations.Add(new Reservation
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com"),
                    //Destination = departure,
                   // PartnerName = partner,
                    //Date = DateTime.Now.AddDays(6),
                    Status = 1
                });

                await _context.SaveChangesAsync();
            }
        }

            private async Task AddFlights()
            {
                if (!_context.Flights.Any())
                {
                    var partner = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();

                    _context.Flights.Add(new Flight
                    {
                        Origin = "Lisboa",
                        Destination = "Porto",
                        DepartureDate = new DateTime(2020, 11, 25, 13, 00, 00),
                        Partner = partner,
                        MaximumSeats = 200,
                        AvailableSeats = 111,
                        Miles = 7000,
                        Status = 0,
                    });

                    await _context.SaveChangesAsync();
                }
            }
        

        private async Task AddPartnership()
        {
            if (!_context.Partners.Any())
            {
                _context.Partners.Add(new Partner
                {
                    CompanyName = "CinelAir Portugal",
                    Address = "Lisboa",
                    Designation = "Air Transport Company",
                    Status = 0
                });
                _context.Partners.Add(new Partner
                {
                    CompanyName = "Tap Air Portugal",
                    Address = "Lisboa",
                    Designation = "Air Transport Company",
                    Status = 0
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddOffers()
        {
            if (!_context.PremiumOffers.Any())
            {
                var partner = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();
                var flight = await _context.Flights.Where(i => i.Id == 1).FirstOrDefaultAsync();
                _context.PremiumOffers.Add(new PremiumOffer
                {
                    Title = "Portugal Aerial Bridge",
                    Flight = flight,
                    Partner = partner,
                    Type = PremiumType.Ticket,
                    Quantity = 10,
                    Price = 7000,
                    Status = 0,
                    Conditions = "Special offer for fear of flying passengers"
                });
                _context.PremiumOffers.Add(new PremiumOffer
                {
                    Title = "All you can eat",
                    Flight = null,
                    Partner = partner,
                    Type = PremiumType.Voucher,
                    Quantity = 50,
                    Price = 10000,
                    Status = 0,
                    Conditions = "Special offer for hungry clients"
                });
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddClientComplaint()
        {
            if (!_context.ClientComplaints.Any())
            {
                _context.ClientComplaints.Add(new ClientComplaint
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com"),
                    Complaint = ComplaintType.Miles,
                    Email = "mariliaa@yopmail.com",
                    Date = DateTime.Now.AddDays(-5),
                    Body = "bla bla bla",
                    Reply = string.Empty,
                    Status = 1

                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddAdvertising()
        {
            if (!_context.Advertisings.Any())
            {
                var partner = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();

                _context.Advertisings.Add(new Advertising
                {
                    Title = "New Promotion",
                    Content = "bla bla bla",
                    EndDate = DateTime.Now.AddMonths(12),
                    Partner = partner,
                    Status = 1
                });

                await _context.SaveChangesAsync();
            }
        }


        public async Task AddTierChanges()
        {
            if (!_context.TierChanges.Any())
            {
                _context.TierChanges.Add(new TierChange
                {
                    OldTier = TierType.Silver,
                    NewTier = TierType.Gold,
                    NumberOfFlights = 3434,
                    NumberOfMiles = 34234,
                    Client = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com"),
                    Status = 1
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task FillUser6Async()
        {
            var user6 = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com");

            if (user6 == null)
            {
                user6 = new User
                {
                    GuidId = _clientRepository.CreateGuid(),
                    Name = "Marilia",
                    Email = "mariliaa@yopmail.com",
                    UserName = "Mariliazinha",
                    PhoneNumber = "965201474",
                    Address = "Rua do teto",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/10/1983"),
                    Gender = "Female",
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "2121218",
                    IsActive = true,
                    IsApproved = true,
                    Status = TierType.Miles,
                    StatusMiles = 500,
                    BonusMiles = 10
                };

                await _userHelper.AddUserAsync(user6, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user6);
                await _userHelper.ConfirmEmailAsync(user6, token);

                var isInRole = await _userHelper.IsUserInRoleAsync(user6, UserType.Client);

                if (!isInRole)
                {
                    await _userHelper.AddUSerToRoleAsync(user6, UserType.Client);
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task FillUser5Async()
        {
            var user5 = await _userHelper.GetUserByEmailAsync("jacintoafonso@yopmail.com");

            if (user5 == null)
            {
                user5 = new User
                {
                    GuidId = _clientRepository.CreateGuid(),
                    Name = "Jacinto",
                    Email = "jacintoafonso@yopmail.com",
                    UserName = "Jacinto",
                    PhoneNumber = "965201474",
                    Address = "Rua do telefone",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/10/1983"),
                    Gender = "Male",
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "21121218",
                    IsActive = true,
                    IsApproved = false,
                    Status = TierType.Gold,
                    StatusMiles = 10000,
                    BonusMiles = 100
                };

                await _userHelper.AddUserAsync(user5, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user5);
                await _userHelper.ConfirmEmailAsync(user5, token);

                var isInRole = await _userHelper.IsUserInRoleAsync(user5, UserType.Client);

                if (!isInRole)
                {
                    await _userHelper.AddUSerToRoleAsync(user5, UserType.Client);
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task FillUser4Async()
        {
            var user4 = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com");

            if (user4 == null)
            {
                user4 = new User
                {
                    GuidId = _clientRepository.CreateGuid(),
                    Name = "Pedro",
                    Email = "estevescardoso@yopmail.com",
                    UserName = "Pedro",
                    PhoneNumber = "965201474",
                    Address = "Rua da taça",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/10/1983"),
                    Gender = "Male",
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "21821218",
                    IsActive = true,
                    IsApproved = false,
                    Status = TierType.Silver,
                    StatusMiles = 120,
                    BonusMiles = 100
                };

                await _userHelper.AddUserAsync(user4, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user4);
                await _userHelper.ConfirmEmailAsync(user4, token);

                var isInRole = await _userHelper.IsUserInRoleAsync(user4, UserType.Client);

                if (!isInRole)
                {
                    await _userHelper.AddUSerToRoleAsync(user4, UserType.Client);
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task FillUser3Async()
        {
            var user = await _userHelper.GetUserByEmailAsync("jpofelix@gmail.com");

            if (user == null)
            {

                user = new User
                {
                    GuidId = _clientRepository.CreateGuid(),
                    Name = "João Felix",
                    Email = "jpofelix@gmail.com",
                    UserName = "JoaoFelix",
                    PhoneNumber = "965201474",
                    Address = "Rua do Ouro",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/10/1983"),
                    Gender = "Male",
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121218",
                    IsActive = false,
                    IsApproved = true,
                    Status = TierType.None,
                    StatusMiles = 0,
                    BonusMiles = 0

                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, UserType.Developer);

            if (!isInRole)
            {
                await _userHelper.AddUSerToRoleAsync(user, UserType.Developer);
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
                    GuidId = _clientRepository.CreateGuid(),
                    Name = "Cátia Oliveira",
                    Email = "catia-96@hotmail.com",
                    UserName = "CatiaOliveira",
                    PhoneNumber = "102547455",
                    Address = "Rua da Luz",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01/09/1997"),
                    Gender = "Female",
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121217",
                    IsActive = true,
                    IsApproved = true,
                    Status = TierType.None,
                    StatusMiles = 0,
                    BonusMiles = 0

                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, UserType.Admin);

            if (!isInRole)
            {
                await _userHelper.AddUSerToRoleAsync(user, UserType.Admin);
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
                    GuidId = _clientRepository.CreateGuid(),
                    Name = "Joana Roque",
                    Email = "joanatpsi@gmail.com",
                    UserName = "JoanaRoque",
                    PhoneNumber = "965214744",
                    Address = "Rua da Programação",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("27/11/1988"),
                    Gender = "Female",
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121212",
                    IsActive = true,
                    IsApproved = true,
                    Status = TierType.None,
                    StatusMiles = 0,
                    BonusMiles = 0
                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, UserType.Developer);

            if (!isInRole)
            {
                var identityResult = await _userHelper.AddUSerToRoleAsync(user, UserType.Developer);
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckOrCreateRoles()
        {
            await _userHelper.CheckRoleAsync(UserType.Developer.ToString());
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.SuperUser.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.Client.ToString());


            await _userHelper.CheckRoleAsync(TierType.None.ToString());
            await _userHelper.CheckRoleAsync(TierType.Miles.ToString());
            await _userHelper.CheckRoleAsync(TierType.Silver.ToString());
            await _userHelper.CheckRoleAsync(TierType.Gold.ToString());
        }

        private void AddCountries(string name)
        {
            _context.Countries.Add(new Country
            {
                Name = name
            });
        }

        private async Task FillCountriesAsync()
        {
            if (!_context.Countries.Any())
            {

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                List<RegionInfo> countriesList = new List<RegionInfo>();
                var countries = new List<Country>();
                foreach (CultureInfo ci in cultures)
                {
                    RegionInfo regionInfo = new RegionInfo(ci.Name);
                    if (countriesList.Count(x => x.EnglishName == regionInfo.EnglishName) <= 0)
                    {
                        countriesList.Add(regionInfo);
                    }
                }

                foreach (RegionInfo regionInfo in countriesList.OrderBy(x => x.EnglishName))
                {
                    var country = regionInfo.EnglishName;
                    AddCountries(country);

                    await _context.SaveChangesAsync();
                }

                AddCountries("null");
            }

        }
    }
}