namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using System;

    public class ConverterHelper : IConverterHelper
    {
        public Reservation ToReservation(ReservationViewModel model, bool isNew)
        {
            var reservation = new Reservation
            {
                Id = isNew ? 0 : model.ReservationId,
                Destination = model.Destination,
                UpdateDate = DateTime.Now,
                Status = 0,
                Date = model.Date
            };

            return reservation;
        }

        public ReservationViewModel ToReservationViewModel(Reservation reservation)
        {
            var reservationClient = new ReservationViewModel
            {
                ReservationId = reservation.Id,
                Destination = reservation.Destination,
                PartnerName = reservation.PartnerName.CompanyName,
                Date = reservation.Date,
                Status = reservation.Status
            };

            return reservationClient;
        }
    }
}
