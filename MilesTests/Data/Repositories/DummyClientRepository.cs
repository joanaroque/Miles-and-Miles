using MilesBackOffice.Web.Models;
using System;
using System.Collections.Generic;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class DummyClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public DummyClientRepository(DataContext context)
        {
            _context = context;

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
                    Date = DateTime.Now.AddDays(-5),
                    Subject = "Help help help",
                    Replay = null,
                    NotProcessed = false
                });

            modelList.Add(
                new ComplaintClientViewModel
                {
                    Id = "2",
                    Name = "Filipe Antonio",
                    Title = "Theft",
                    Date = DateTime.Now.AddDays(-2),
                    Subject = "Help help help",
                    Replay = null,
                    NotProcessed = false

                });

            modelList.Add(
                new ComplaintClientViewModel
                {
                    Id = "3",
                    Name = "Alfredo Amarelo",
                    Title = "Theft",
                    Date = DateTime.Now.AddDays(-1),
                    Subject = "Help help help",
                    Replay = null,
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
    }
}
