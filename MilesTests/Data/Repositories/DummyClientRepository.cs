using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class DummyClientRepository : IClientRepository
    {
        private readonly IUserHelper _userHelper;

        public DummyClientRepository(IUserHelper userHelper)
        {
            _userHelper = userHelper;

        }

        public async Task<List<AdvertisingViewModel>> GetAdvertisingToBeConfirm()
        {
            List<AdvertisingViewModel> modelList = new List<AdvertisingViewModel>();

            var user4 = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com");

            modelList.Add(
                new AdvertisingViewModel
                {
                    AdvertisingId = "11",
                    UserId = user4.Id.ToString(),
                    Title = "miles and miles",
                    Content = "this is very nice",
                    EndDate = DateTime.Now.AddMonths(1),
                    PartnerId = "22",
                    IsConfirm = false
                });
            var user5 = await _userHelper.GetUserByEmailAsync("jacintoafonso@yopmail.com");

            modelList.Add(
                new AdvertisingViewModel
                {
                    AdvertisingId = "22",
                    UserId = user5.Id.ToString(),
                    Title = "miles and miles and miles",
                    Content = "this is very nice indeed",
                    EndDate = DateTime.Now.AddMonths(2),
                    PartnerId = "25",
                    IsConfirm = false

                });

            var user6 = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com");

            modelList.Add(
                new AdvertisingViewModel
                {
                    AdvertisingId = "33",
                    UserId = user6.Id.ToString(),
                    Title = "miles and miles and miles and miles",
                    Content = "this is very nice twice",
                    EndDate = DateTime.Now.AddDays(25),
                    PartnerId = "12",
                    IsConfirm = false

                });

            return modelList;
        }

        public async Task<List<ComplaintClientViewModel>> GetClientComplaints()
        {
            List<ComplaintClientViewModel> modelList = new List<ComplaintClientViewModel>();

            var user4 = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com");

            modelList.Add(
                new ComplaintClientViewModel
                {
                    ComplaintId = "1",
                    UserId = user4.Id.ToString(),
                    Name = "Jeremias Matos",
                    Title = "Theft",
                    Email = "sdjklncj34@yopmail.com",
                    Date = DateTime.Now.AddDays(-5),
                    Subject = "Help help help",
                    Reply = null,
                    IsProcessed = false
                });

            var user5 = await _userHelper.GetUserByEmailAsync("jacintoafonso@yopmail.com");

            modelList.Add(
                new ComplaintClientViewModel
                {
                    ComplaintId = "2",
                    UserId = user5.Id.ToString(),
                    Name = "Filipe Antonio",
                    Title = "Theft",
                    Email = "43tref@yopmail.com",
                    Date = DateTime.Now.AddDays(-2),
                    Subject = "Help help help",
                    Reply = null,
                    IsProcessed = false

                });

            var user6 = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com");

            modelList.Add(
                new ComplaintClientViewModel
                {
                    ComplaintId = "3",
                    UserId = user6.Id.ToString(),
                    Name = "Alfredo Amarelo",
                    Title = "Theft",
                    Email = "bghnjyu89@yopmail.com",
                    Date = DateTime.Now.AddDays(-1),
                    Subject = "Help help help",
                    Reply = null,
                    IsProcessed = false

                });

            return modelList;

        }

        public  async Task<List<TierChangeViewModel>> GetPendingTierClient()
        {
            List<TierChangeViewModel> modelList = new List<TierChangeViewModel>();

            var user4 = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com");
           
            modelList.Add(
                new TierChangeViewModel
                {
                    UserId = user4.Id.ToString(),
                    Name = "Pedro",
                    OldTier = "Silver",
                    NewTier = "Gold",
                    NumberOfFlights = 110,
                    NumberOfMiles = 105000,
                    IsConfirm = false
                });

            var user5 = await _userHelper.GetUserByEmailAsync("jacintoafonso@yopmail.com");
            modelList.Add(
                new TierChangeViewModel
                {
                    UserId = user5.Id.ToString(),
                    Name = "Jacinto",
                    OldTier = "Silver",
                    NewTier = "Gold",
                    NumberOfFlights = 105,
                    NumberOfMiles = 100010,
                    IsConfirm = false

                });

            var user6 = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com");

            modelList.Add(
                new TierChangeViewModel
                {
                    UserId = user6.Id.ToString(),
                    Name = "Marilia",
                    OldTier = "Silver",
                    NewTier = "Gold",
                    NumberOfFlights = 160,
                    NumberOfMiles = 100800,
                    IsConfirm = false

                });

            return modelList;
        }

        public async Task<List<AvailableSeatsViewModel>> GetSeatsToBeConfirm()
        {
            List<AvailableSeatsViewModel> modelList = new List<AvailableSeatsViewModel>();

            var user4 = await _userHelper.GetUserByEmailAsync("estevescardoso@yopmail.com");

            modelList.Add(
                new AvailableSeatsViewModel
                {
                    UserId = user4.Id.ToString(),
                    Name = "Maria",
                    FlightNumber = 147,
                    NumberOfSeats = 400,
                    AvailableSeats = 10,
                    IsConfirm = false
                });

            var user5 = await _userHelper.GetUserByEmailAsync("jacintoafonso@yopmail.com");

            modelList.Add(
                new AvailableSeatsViewModel
                {
                    UserId = user5.Id.ToString(),
                    Name = "Antonia",
                    FlightNumber = 258,
                    NumberOfSeats = 350,
                    AvailableSeats = 30,
                    IsConfirm = false

                });

            var user6 = await _userHelper.GetUserByEmailAsync("mariliaa@yopmail.com");

            modelList.Add(
                new AvailableSeatsViewModel
                {
                    UserId = user6.Id.ToString(),
                    Name = "Manuela",
                    FlightNumber = 963,
                    NumberOfSeats = 300,
                    AvailableSeats = 20,
                    IsConfirm = false

                });

            return modelList;
        }
    }
}
