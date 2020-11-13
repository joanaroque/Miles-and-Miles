using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using CinelAirMilesLibrary.Common.Data;
using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Enums;
using CinelAirMilesLibrary.Common.Helpers;

using Microsoft.EntityFrameworkCore;

namespace MilesBackOffice.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
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

            await AddPartnership();

            await AddClientComplaint();

            await AddOffers();

            await AddFlights();

            await AddAdvertising();

            await AddNotifications();
        }

        private async Task AddNotifications()
        {
            if (!_context.Notifications.Any())
            {
                var partner = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("joanatpsi@gmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 1,
                    Type = NotificationType.Complaint,
                    Message = "Hey Joana, please check the account of client X",
                    ItemId = GuidHelper.CreatedGuid()
                });

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("joanatpsi@gmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 1,
                    Type = NotificationType.Complaint,
                    Message = "Joana please don't fall asleep while programming!!",
                    ItemId = GuidHelper.CreatedGuid()
                });

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 1,
                    Type = NotificationType.Complaint,
                    Message = "Come and see our offers and promotions!",
                    ItemId = GuidHelper.CreatedGuid()
                });

                _context.Notifications.Add(new Notification
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com"),
                    CreateDate = DateTime.Now.AddDays(-8).AddHours(1).AddMinutes(1).AddSeconds(1),
                    Status = 1,
                    Type = NotificationType.Complaint,
                    Message = "Please check your tier increase in our Cinel Air Miles program!",
                    ItemId = GuidHelper.CreatedGuid()
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
                    Departure = "Lisboa",
                    Arrival = "Paris",
                    DepartureDate = new DateTime(2020, 09, 01, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 110,
                    AvailableSeats = 70,
                    Status = 0,
                    Distance = 1735
                });
                _context.Flights.Add(new Flight
                {
                    Departure = "Lisboa",
                    Arrival = "Paris",
                    DepartureDate = new DateTime(2020, 09, 12, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 110,
                    AvailableSeats = 70,
                    Status = 0,
                    Distance = 1735
                });
                _context.Flights.Add(new Flight
                {
                    Departure = "Lisboa",
                    Arrival = "Paris",
                    DepartureDate = new DateTime(2020, 09, 21, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 110,
                    AvailableSeats = 70,
                    Status = 0,
                    Distance = 1735
                });
                _context.Flights.Add(new Flight
                {
                    Departure = "Lisboa",
                    Arrival = "Paris",
                    DepartureDate = new DateTime(2020, 09, 30, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 110,
                    AvailableSeats = 70,
                    Status = 0,
                    Distance = 1735
                });

                _context.Flights.Add(new Flight
                {
                    Departure = "Madrid",
                    Arrival = "Lisboa",
                    DepartureDate = new DateTime(2020, 11, 16, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 150,
                    AvailableSeats = 55,
                    Status = 0,
                    Distance = 623
                });

                _context.Flights.Add(new Flight
                {
                    Departure = "Tokyo",
                    Arrival = "Lisboa",
                    DepartureDate = new DateTime(2021, 11, 16, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 150,
                    AvailableSeats = 55,
                    Status = 0,
                    Distance = 15381
                });

                _context.Flights.Add(new Flight
                {
                    Departure = "Lisboa",
                    Arrival = "Xangai",
                    DepartureDate = new DateTime(2022, 11, 16, 13, 00, 00),
                    Partner = partner,
                    MaximumSeats = 150,
                    AvailableSeats = 55,
                    Status = 0,
                    Distance = 13639
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
                    Status = 0,
                    PartnerGuidId = GuidHelper.CreatedGuid()
                });
                _context.Partners.Add(new Partner
                {
                    CompanyName = "Tap Air Portugal",
                    Address = "Lisboa",
                    Designation = "Air Transport Company",
                    Status = 0,
                    PartnerGuidId = GuidHelper.CreatedGuid()
                });
                _context.Partners.Add(new Partner
                {
                    CompanyName = "Vila Vita Hotel & SPA",
                    Address = "Algarve",
                    Designation = "Hotel",
                    Status = 0,
                    PartnerGuidId = GuidHelper.CreatedGuid()
                });
                _context.Partners.Add(new Partner
                {
                    CompanyName = "Sobreiras - Alentejo Country Hotel",
                    Address = "Alentejo",
                    Designation = "Hotel",
                    Status = 0,
                    PartnerGuidId = GuidHelper.CreatedGuid()
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
                    Status = 1,
                    Conditions = "Special offer for fear of flying passengers",
                    OfferIdGuid = GuidHelper.CreatedGuid()
                });
                _context.PremiumOffers.Add(new PremiumOffer
                {
                    Title = "All you can eat",
                    Flight = null,
                    Partner = partner,
                    Type = PremiumType.Ticket,
                    Quantity = 50,
                    Price = 10000,
                    Status = 1,
                    Conditions = "Special offer for hungry clients",
                    OfferIdGuid = GuidHelper.CreatedGuid()
                });
                partner = await _context.Partners.Where(p => p.CompanyName == "Vila Vita Hotel & SPA").FirstOrDefaultAsync();
                _context.PremiumOffers.Add(new PremiumOffer
                {
                    Title = "Vila Vita Parc",
                    Flight = null,
                    Partner = partner,
                    Type = PremiumType.Voucher,
                    Quantity = 50,
                    Price = 10000,
                    Status = 1,
                    Conditions = "Considered one of the best resorts in Europe, " +
                    "Vila Vita Parc Resort & Spa is located on a 22-hectare property" +
                    " with lush sub-tropical gardens overlooking the Algarve coast and the Atlantic Ocean." +
                    "With the Cinel Air Miles Program you can enjoy your stay and earn and use miles.",
                    OfferIdGuid = GuidHelper.CreatedGuid()
                });
                partner = await _context.Partners.Where(p => p.CompanyName == "Sobreiras - Alentejo Country Hotel").FirstOrDefaultAsync();
                _context.PremiumOffers.Add(new PremiumOffer
                {
                    Title = "GRÂNDOLA | CHARM STAY",
                    Flight = null,
                    Partner = partner,
                    Type = PremiumType.Voucher,
                    Quantity = 50,
                    Price = 10000,
                    Status = 1,
                    Conditions = "Sobreiras - Alentejo Country Hotel offers a unique experience of " +
                    "tranquility and leisure with Nature at 360º, combining a simple and elegant design " +
                    "inspired by the Alentejo landscape. It is the perfect getaway away from city life and " +
                    "confusion and is just an hour away from Lisbon and just minutes from Vila de Grândola. ",
                    OfferIdGuid = GuidHelper.CreatedGuid()
                });

                await _context.SaveChangesAsync();
            }
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
                    Body = "TAP Air Portugal fraud!! I booked a group of 8 people " +
                    "on TAP Toronto-Lisbon and prepaid for seat selection " +
                    "($44 per seat in each direction - total $704). When we checked in, we were scattered " +
                    "around the plane and the seats we paid for were assigned to other passengers. " +
                    "The same thing happened on the waý back!I lodged a complaint with TAP online " +
                    "(#172341) and was promised a reply within 28 days. Two months, and several " +
                    "long-distance calls and emails later, TAP Complaints Dept. say they are still" +
                    " unable to give me an answer.By chance, I was talking to someone about this " +
                    "last night, and he had the identical experience. If this is systemic, it is fraud! TAP charge for a service" +
                    " they do not provide.I would like to hear from others who have had the same experience, because I intend " +
                    "to escalate this matter, and to warn travellers NOT TO PREPAY FOR SERVICES ON TAP!!",
                    Reply = string.Empty,
                    Status = 1
                });

                _context.ClientComplaints.Add(new ClientComplaint
                {
                    CreatedBy = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com"),
                    Complaint = ComplaintType.Miles,
                    Email = "mariliaa@yopmail.com",
                    Date = DateTime.Now.AddDays(-5),
                    Body = "After canceling my flight to Brazil due to the Covid-19 pandemic in May / 2020," +
                    " I received 6 vouchers (I had two reservations for three tickets each), I intend to receive the" +
                    " refund and not vouchers since I would go to the wedding of a family member now without reason to travel" +
                    " and consequent application of the referred vouchers.I placed a refund request through the tap online system," +
                    " I received a reply saying that my situation was resolved once I received the vouchers.Then I made(in May) a complaint due to the response received," +
                    "  and I still question what I have to do to receive the money related to the vouchers, as required by the rules defined in the European Union." +
                    "To date I have not received a reply and the online feedback of the complaint on the Tap portal indicates that the complaint is under analysis," +
                    " with TAP with money that does not belong to it for a month and a half.",
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
                var partner1 = await _context.Partners.Where(p => p.CompanyName == "CinelAir Portugal").FirstOrDefaultAsync();
                var partner2 = await _context.Partners.Where(p => p.CompanyName == "Tap Air Portugal").FirstOrDefaultAsync();
                var user = await _userHelper.GetUserByEmailAsync("jpofelix@gmail.com");

                _context.Advertisings.Add(new Advertising
                {
                    Title = "New Promotion",
                    Content = "November is the month to celebrate the 2nd anniversary of the Cinel Air Miles Program."
                    + "2 years have passed since the Cinel Air Miles Program was born, and the best way to celebrate is"
                    + "with exclusive offers! During the month of November you can find on this page all the surprises that we"
                    + "- together with our partners - have prepared to celebrate this very special date." +
                    "There were 24 months of fantastic adventures, " +
                    " good times and, in the last year,  some less good ones. " +
                    "However, we remain positive and eager to fly with you on board, with more confidence and confidence than ever." +
                    "Time starts to fly.Enjoy every mile.",
                    EndDate = DateTime.Now.AddMonths(12),
                    Partner = partner1,
                    ImageUrl = ("~/images/advertisings/miles2.jpg"),
                    Status = 1,
                    PostGuidId = GuidHelper.CreatedGuid(),
                    CreatedBy = user,
                    CreateDate = DateTime.Now

                });

                _context.Advertisings.Add(new Advertising
                {
                    Title = "TAP Miles&Go Pets",
                    Content = "Because we are a Pet Friendly company," +
                    " our TAP Miles & Go Customers earn miles when traveling on TAP Air Portugal " +
                    "with their pets. Whether in the cabin or in the hold, you can get up to 500" +
                    " miles for taking your faithful friend on our planes!",
                    EndDate = DateTime.Now.AddMonths(12),
                    Partner = partner2,
                    ImageUrl = ("~/images/advertisings/TAP-MilesGO-Pets.jpg"),
                    Status = 1,
                    PostGuidId = GuidHelper.CreatedGuid(),
                    CreatedBy = user,
                    CreateDate = DateTime.Now
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
                    GuidId = GuidHelper.CreatedGuid(),
                    Name = "Marilia Amélia Santos",
                    Email = "mariliaa@yopmail.com",
                    UserName = "Mariliazinha",
                    PhoneNumber = "965201474",
                    Address = "Rua do teto",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1945, 11, 20),
                    Gender = Gender.Female,
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "240355879",
                    IsActive = true,
                    IsApproved = true,
                    Tier = TierType.Basic,
                    StatusMiles = 0,
                    BonusMiles = 0,
                    SelectedRole = UserType.Client
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
                    GuidId = GuidHelper.CreatedGuid(),
                    Name = "Jacinto Simões Costa",
                    Email = "jacintoafonso@yopmail.com",
                    UserName = "JacintoSC83",
                    PhoneNumber = "965201474",
                    Address = "Rua do telefone",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1999, 03, 21),
                    Gender = Gender.Male,
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "230654789",
                    IsActive = true,
                    IsApproved = true,
                    Tier = TierType.Basic,
                    StatusMiles = 0,
                    BonusMiles = 0,
                    SelectedRole = UserType.Client
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
                    GuidId = GuidHelper.CreatedGuid(),
                    Name = "Pedro Esteves Cardoso",
                    Email = "estevescardoso@yopmail.com",
                    UserName = "PedroCardoso",
                    PhoneNumber = "965201474",
                    Address = "Rua da taça",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1987, 05, 03),
                    Gender = Gender.Male,
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "21821218",
                    IsActive = true,
                    IsApproved = true,
                    Tier = TierType.Gold,
                    StatusMiles = 70000,
                    BonusMiles = 70000,
                    SelectedRole = UserType.Client
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
                    GuidId = GuidHelper.CreatedGuid(),
                    Name = "João Felix",
                    Email = "jpofelix@gmail.com",
                    UserName = "JoaoFelix",
                    PhoneNumber = "965201474",
                    Address = "Rua do Ouro",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1983, 10, 01),
                    Gender = Gender.Male,
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121218",
                    IsActive = true,
                    IsApproved = true,
                    Tier = TierType.None,
                    StatusMiles = 0,
                    BonusMiles = 0,
                    SelectedRole = UserType.User
                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, UserType.User);

            if (!isInRole)
            {
                await _userHelper.AddUSerToRoleAsync(user, UserType.User);
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
                    GuidId = GuidHelper.CreatedGuid(),
                    Name = "Cátia Oliveira",
                    Email = "catia-96@hotmail.com",
                    UserName = "CatiaOliveira",
                    PhoneNumber = "102547455",
                    Address = "Rua da Luz",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1997, 09, 01),
                    Gender = Gender.Female,
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121217",
                    IsActive = true,
                    IsApproved = true,
                    Tier = TierType.None,
                    StatusMiles = 0,
                    BonusMiles = 0,
                    SelectedRole = UserType.Admin
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
                    GuidId = GuidHelper.CreatedGuid(),
                    Name = "Joana Roque",
                    Email = "joanatpsi@gmail.com",
                    UserName = "JoanaRoque",
                    PhoneNumber = "965214744",
                    Address = "Rua da Programação",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1988, 11, 27),
                    Gender = Gender.Female,
                    City = "Lisbon",
                    Country = await _context.Countries.Where(c => c.Name == "Portugal").FirstOrDefaultAsync(),
                    TIN = "212121212",
                    IsActive = true,
                    IsApproved = true,
                    Tier = TierType.None,
                    StatusMiles = 0,
                    BonusMiles = 0,
                    SelectedRole = UserType.SuperUser
                };

                await _userHelper.AddUserAsync(user, "123456");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, UserType.SuperUser);

            if (!isInRole)
            {
                var identityResult = await _userHelper.AddUSerToRoleAsync(user, UserType.SuperUser);
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