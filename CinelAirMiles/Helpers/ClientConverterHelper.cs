namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;

    using CinelAirMilesLibrary.Common.Data.Entities;

    using System;

    public class ClientConverterHelper : IClientConverterHelper
    {
        public ClientComplaint ToClientComplaint(ComplaintViewModel model, bool isNew)
        {
            var client = new ClientComplaint
            {
                Id = isNew ? 0 : model.Id,
                Complaint = model.Complaint,
                Email = model.Email,
                Date = model.Date,
                Body = model.Body,
                Status = model.Status,
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
                
            };

            return complaint;
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
              //todo   ReservationID = isNew ? null : model.ReservationId,
                UpdateDate = DateTime.Now,
                Status = 0,
                CreateDate = model.DepartureDate
            };

            return reservation;
        }

        public ReservationViewModel ToReservationViewModel(Reservation reservation)
        {
            var reservationClient = new ReservationViewModel
            {
                ReservationId = reservation.ReservationID.ToString(),
                Departure = reservation.MyPremium.Flight.Destination,
                PartnerName = reservation.MyPremium.Partner.CompanyName,
                DepartureDate = reservation.CreateDate,
                Status = reservation.Status,
                Name = reservation.CreatedBy.Name
            };

            return reservationClient;
        }
    }
}
