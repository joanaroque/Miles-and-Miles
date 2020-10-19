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
                CreatedBy = model.CreatedBy,
                CreateDate = isNew ? DateTime.Now : model.CreateDate,
                UpdateDate = DateTime.Now,
                PendingPublish = model.PendingPublish,
                Status = model.Status
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
                CreatedBy = advertising.CreatedBy,
                CreateDate = advertising.CreateDate,
                UpdateDate = DateTime.Now,
                PendingPublish = advertising.PendingPublish,
                Status = advertising.Status,
                ModifiedBy = advertising.ModifiedBy
            };
            return advertisings;
        }

        public AvailableSeatsViewModel ToAvailableSeatsViewModel(SeatsAvailable seatsAvailable)
        {
            var seats = new AvailableSeatsViewModel
            {
                Id = seatsAvailable.Id,
                CreatedBy = seatsAvailable.CreatedBy,
                CreateDate = seatsAvailable.CreateDate,
                UpdateDate = DateTime.Now,
                ConfirmSeatsAvailable = seatsAvailable.ConfirmSeatsAvailable,
                Status = seatsAvailable.Status,
                ModifiedBy = seatsAvailable.ModifiedBy,
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
                CreatedBy = model.CreatedBy,
                CreateDate = isNew ? DateTime.Now : model.CreateDate,
                UpdateDate = DateTime.Now,
                PendingComplaint = model.PendingComplaint,
                Status = model.Status,
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
                CreatedBy = clientComplaint.CreatedBy,
                CreateDate = clientComplaint.CreateDate,
                UpdateDate = DateTime.Now,
                PendingComplaint = clientComplaint.PendingComplaint,
                Status = clientComplaint.Status,
                ModifiedBy = clientComplaint.ModifiedBy,
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
                Id = isNew ? 0 : model.Id,
                CreatedBy = model.CreatedBy,
                CreateDate = isNew ? DateTime.Now : model.CreateDate,
                UpdateDate = DateTime.Now,
                FlightNumber = model.FlightNumber,
                MaximumSeats = model.MaximumSeats,
                AvailableSeats = model.AvailableSeats,
                ConfirmSeatsAvailable = model.ConfirmSeatsAvailable,
                Status = model.Status

            };

            return seats;
        }

        public TierChange ToTierChange(TierChangeViewModel model, bool isNew)
        {
            var tier = new TierChange
            {
                Id = isNew ? 0 : model.Id,
                CreatedBy = model.CreatedBy,
                CreateDate = isNew ? DateTime.Now : model.CreateDate,
                UpdateDate = DateTime.Now,
                OldTier = model.OldTier,
                NewTier = model.NewTier,
                NumberOfFlights = model.NumberOfFlights,
                NumberOfMiles = model.NumberOfMiles,
                IsConfirm = model.IsConfirm,
                Status = model.Status

            };

            return tier;
        }

        public TierChangeViewModel ToTierChangeViewModel(TierChange tierChange)
        {
            var tierChanges = new TierChangeViewModel
            {
                Id = tierChange.Id,
                CreatedBy = tierChange.CreatedBy,
                CreateDate = tierChange.CreateDate,
                UpdateDate = DateTime.Now,
                OldTier = tierChange.OldTier,
                NewTier = tierChange.NewTier,
                NumberOfFlights = tierChange.NumberOfFlights,
                NumberOfMiles = tierChange.NumberOfMiles,
                IsConfirm = tierChange.IsConfirm,
                Status = tierChange.Status
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
