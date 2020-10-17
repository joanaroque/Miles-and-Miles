using System;
using System.Collections.Generic;

using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class DummyClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public DummyClientRepository(DataContext context)
        {
            _context = context;

        }

        public List<AdvertisingViewModel> GetAdvertisingToBeConfirm()
        {
            List<AdvertisingViewModel> modelList = new List<AdvertisingViewModel>();
            modelList.Add(
                new AdvertisingViewModel
                {
                    Id = "1",
                    Title = "miles and miles",
                    Content = "this is very nice",
                    EndDate = DateTime.Now.AddMonths(1),
                    PartnerId = "22",
                    IsConfirm = false
                });

            modelList.Add(
                new AdvertisingViewModel
                {
                    Id = "2",
                    Title = "miles and miles and miles",
                    Content = "this is very nice indeed",
                    EndDate = DateTime.Now.AddMonths(2),
                    PartnerId = "25",
                    IsConfirm = false

                });

            modelList.Add(
                new AdvertisingViewModel
                {
                    Id = "3",
                    Title = "miles and miles and miles and miles",
                    Content = "this is very nice twice",
                    EndDate = DateTime.Now.AddDays(25),
                    PartnerId = "12",
                    IsConfirm = false

                });

            return modelList;
        }

        public List<ComplaintClientViewModel> GetClientComplaint()
        {
            List<ComplaintClientViewModel> modelList = new List<ComplaintClientViewModel>();
            modelList.Add(
                new ComplaintClientViewModel
                {
                    Id = "1",
                    Name = "Jeremias Matos",
                    Title = "Theft",
                    Email = "sdjklncj34@yopmail.com",
                    Date = DateTime.Now.AddDays(-5),
                    Subject = "Help help help",
                    Reply = null,
                    NotProcessed = false
                });

            modelList.Add(
                new ComplaintClientViewModel
                {
                    Id = "2",
                    Name = "Filipe Antonio",
                    Title = "Theft",
                    Email = "43tref@yopmail.com",
                    Date = DateTime.Now.AddDays(-2),
                    Subject = "Help help help",
                    Reply = null,
                    NotProcessed = false

                });

            modelList.Add(
                new ComplaintClientViewModel
                {
                    Id = "3",
                    Name = "Alfredo Amarelo",
                    Title = "Theft",
                    Email = "bghnjyu89@yopmail.com",
                    Date = DateTime.Now.AddDays(-1),
                    Subject = "Help help help",
                    Reply = null,
                    NotProcessed = false

                });

            return modelList;

        }

        public List<TierChangeViewModel> GetPendingTierClient()
        {

            List<TierChangeViewModel> modelList = new List<TierChangeViewModel>();
            modelList.Add(
                new TierChangeViewModel
                {
                    Id = "1",
                    Name = "Maria",
                    OldTier = "Silver",
                    NewTier = "Gold",
                    NumberOfFlights = 110,
                    NumberOfMiles = 105000,
                    IsConfirm = false
                });

            modelList.Add(
                new TierChangeViewModel
                {
                    Id = "2",
                    Name = "Antonia",
                    OldTier = "Silver",
                    NewTier = "Gold",
                    NumberOfFlights = 105,
                    NumberOfMiles = 100010,
                    IsConfirm = false

                });

            modelList.Add(
                new TierChangeViewModel
                {
                    Id = "3",
                    Name = "Manuela",
                    OldTier = "Silver",
                    NewTier = "Gold",
                    NumberOfFlights = 160,
                    NumberOfMiles = 100800,
                    IsConfirm = false

                });

            return modelList;
        }

        public List<AvailableSeatsViewModel> GetSeatsToBeConfirm()
        {
            List<AvailableSeatsViewModel> modelList = new List<AvailableSeatsViewModel>();
            modelList.Add(
                new AvailableSeatsViewModel
                {
                    Id = "1",
                    Name = "Maria",
                    FlightNumber = 147,
                    NumberOfSeats = 400,
                    AvailableSeats = 10,
                    IsConfirm = false
                });

            modelList.Add(
                new AvailableSeatsViewModel
                {
                    Id = "2",
                    Name = "Antonia",
                    FlightNumber = 258,
                    NumberOfSeats = 350,
                    AvailableSeats = 30,
                    IsConfirm = false

                });

            modelList.Add(
                new AvailableSeatsViewModel
                {
                    Id = "3",
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
