namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Helpers;
    using System;

    public class ClientConverterHelper : IClientConverterHelper
    {

        public ClientConverterHelper()
        {
        }

        public AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising)
        {
            var advertisings = new AdvertisingViewModel
            {
                Id = advertising.Id,
                Title = advertising.Title,
                Content = advertising.Content,
                Image = advertising.Image,
                EndDate = advertising.EndDate,
                Status = advertising.Status,
                //PartnerName = advertising.Partner.CompanyName,
                PostGuidId = advertising.PostGuidId,
                CreatedBy = advertising.CreatedBy == null ? "unknown" : advertising.CreatedBy.Name,
                CreatedOn = advertising.CreateDate
            };
            return advertisings;
        }

        public ClientComplaint ToClientComplaint(ComplaintViewModel model, bool isNew, User user)
        {
            var client = new ClientComplaint
            {
                Id = isNew ? 0 : model.Id,
                Complaint = model.Complaint,
                Email = user.Email,
                Date = DateTime.UtcNow,
                Body = model.Body,
                Status = 1,
                CreateDate = DateTime.UtcNow,
                CreatedBy = user
            };

            return client;
        }

        public ComplaintViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint)
        {
            var complaint = new ComplaintViewModel
            {
                Id = clientComplaint.Id,
                Complaint = clientComplaint.Complaint,
                Email = clientComplaint.CreatedBy.Email,
                Date = clientComplaint.CreateDate,
                Body = clientComplaint.Body,
                Status = clientComplaint.Status,
                Reply = clientComplaint.Reply
            };

            return complaint; // todo diz que a lista vem a null
        }

        public Notification ToNotification(NotificationViewModel model, bool isNew)
        {
            var notification = new Notification
            {
                Id = isNew ? 0 : model.NotiId,
                UpdateDate = DateTime.Now,
                Status = 8,
                CreateDate = model.Date,
                Message = model.Message

            };

            return notification;
        }

        public NotificationViewModel ToNotificationViewModel(Notification notification)
        {
            var notificationModel = new NotificationViewModel
            {
                NotiId = notification.Id,
                Status = notification.Status,
                Message = notification.Message,
                Date = notification.CreateDate,
                ClientName = notification.CreatedBy.Name
            };

            return notificationModel;
        }

        public Reservation ToReservation(ReservationViewModel model, bool isNew)
        {
            var reservation = new Reservation
            {
                //Id = isNew ? 0 : model.ReservationId,
                //Destination = model.Destination,
                UpdateDate = DateTime.Now,
                Status = 0,
                //Date = model.Date
            };

            return reservation;
        }

        public ReservationViewModel ToReservationViewModel(Reservation reservation)
        {
            var reservationClient = new ReservationViewModel
            {
                ReservationId = reservation.Id.ToString(),
                //Destination = reservation.Destination,
                //PartnerName = reservation.PartnerName.CompanyName,
                //Date = reservation.Date,
                Status = reservation.Status,
                //Name = reservation.CreatedBy.Name
            };

            return reservationClient;
        }


        public TransactionViewModel ToTransactionViewModel(Transaction transaction, User user)
        {
            var model = new TransactionViewModel
            {
                Id = transaction.Id,
                EndBalance = transaction.EndBalance,
                //Todo PremiumOffer = transaction.Product.Title,
                Price = transaction.Price,
                StartBalance = transaction.StartBalance,
                TransferTo = transaction.TransferTo,
                Type = transaction.TransactionType,
                Value = transaction.Value
            };

            return model;
        }


        public User ToUser(RegisterNewUserViewModel model, Country country)
        {
            return new User
            {
                GuidId = GuidHelper.CreatedGuid(),
                Name = model.Name,
                Email = model.EmailAddress,
                UserName = model.Username,
                Country = country,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                TIN = model.TIN,
                SelectedRole = UserType.Client,
                IsActive = true,
                IsApproved = false,
                EmailConfirmed = false,
                Tier = TierType.Basic
            };
        }


        public PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model)
        {
            var offer = new PremiumOfferViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Conditions = string.IsNullOrEmpty(model.Conditions) ? string.Empty : model.Conditions,
                Quantity = model.Quantity,
                Price = model.Price,
                Type = model.Type,
                Image = model.Image,
                PartnerName = model.Partner.CompanyName,
                OfferGuidId = model.OfferIdGuid,
                
            };
            if (model.Flight != null)
            {
                offer.AvailableSeats = model.Flight == null ? -1 : model.Flight.AvailableSeats;
                offer.Arrival = string.IsNullOrEmpty(model.Flight.Arrival) ? string.Empty : model.Flight.Arrival;
                offer.Departure = string.IsNullOrEmpty(model.Flight.Departure) ? string.Empty : model.Flight.Departure;
                offer.FlightDateTime = model.Flight.DepartureDate;
            }

            return offer;
        }
    }
}
