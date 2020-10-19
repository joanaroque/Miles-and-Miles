using System;

using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Enums;
using MilesBackOffice.Web.Models.SuperUser;
using MilesBackOffice.Web.Models.User;

namespace MilesBackOffice.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public ConverterHelper()
        {

        }

        public Advertising ToAdvertising(AdvertisingViewModel model, Guid imageId, bool isNew)
        {
            var advertisng = new Advertising
            {
                Id = isNew ? 0 : model.AdvertisingId,
                Title = model.Title,
                Content = model.Content,
                ImageId = imageId,
                EndDate = model.EndDate,
                UpdateDate = DateTime.Now,
                PendingPublish = model.PendingPublish,
                Status = 1
            };

            return advertisng;
        }

        public AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising)
        {
            var advertisings = new AdvertisingViewModel
            {
                AdvertisingId = advertising.Id,
                Title = advertising.Title,
                Content = advertising.Content,
                ImageId = advertising.ImageId,
                EndDate = advertising.EndDate,
                PendingPublish = advertising.PendingPublish,
            };
            return advertisings;
        }

        public AvailableSeatsViewModel ToAvailableSeatsViewModel(SeatsAvailable seatsAvailable)
        {
            var seats = new AvailableSeatsViewModel
            {
                FlightId = seatsAvailable.Id,
                ConfirmSeatsAvailable = seatsAvailable.ConfirmSeatsAvailable,
                MaximumSeats = seatsAvailable.MaximumSeats,
                FlightNumber = seatsAvailable.FlightNumber,
                AvailableSeats = seatsAvailable.AvailableSeats
            };
            return seats;
        }

        public ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew)
        {
            var complaint = new ClientComplaint
            {
                Id = isNew ? 0 : model.ComplaintId,
                UpdateDate = DateTime.Now,
                Status = 1,
                Title = model.Title,
                Email = model.Email,
                Date = model.Date,
                Subject = model.Subject,
                Reply = model.Reply

            };

            return complaint;
        }

        public ComplaintClientViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint)
        {
            var complaint = new ComplaintClientViewModel
            {
                ComplaintId = clientComplaint.Id,
                Title = clientComplaint.Title,
                Email = clientComplaint.Email,
                Date = clientComplaint.Date,
                Subject = clientComplaint.Subject,
                Reply = clientComplaint.Reply

            };

            return complaint;
        }

        public SeatsAvailable ToSeatsAvailable(AvailableSeatsViewModel model, bool isNew)
        {
            var seats = new SeatsAvailable
            {
                Id = isNew ? 0 : model.FlightId,
                UpdateDate = DateTime.Now,
                FlightNumber = model.FlightNumber,
                MaximumSeats = model.MaximumSeats,
                AvailableSeats = model.AvailableSeats,
                ConfirmSeatsAvailable = model.ConfirmSeatsAvailable,
                Status = 1
            };

            return seats;
        }

        public TierChange ToTierChange(TierChangeViewModel model, bool isNew)
        {
            var tier = new TierChange
            {
                Id = isNew ? 0 : model.TierChangeId,
                UpdateDate = DateTime.Now,
                OldTier = model.OldTier,
                NewTier = model.NewTier,
                NumberOfFlights = model.NumberOfFlights,
                NumberOfMiles = model.NumberOfMiles,
                IsConfirm = model.IsConfirm,
                Status = 1
            };

            return tier;
        }

        public TierChangeViewModel ToTierChangeViewModel(TierChange tierChange)
        {
            var tierChanges = new TierChangeViewModel
            {
                TierChangeId = tierChange.Id,
                OldTier = tierChange.OldTier,
                NewTier = tierChange.NewTier,
                NumberOfFlights = tierChange.NumberOfFlights,
                NumberOfMiles = tierChange.NumberOfMiles,
                IsConfirm = tierChange.IsConfirm
            };
            return tierChanges;
        }


        public PremiumOffer ToPremiumTicket(CreateTicketViewModel model)
        {
            return new PremiumOffer
            {
                Flight = model.FlightId.ToString(),
                Title = model.Title,
                Quantity = model.Quantity,
                Price = model.Price,
                Type = PremiumType.Ticket,
                Status = 1
            };
        }
    }
}
