namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;
    using CinelAirMilesLibrary.Common.Data.Entities;


    public interface IConverterHelper
    {
        Reservation ToReservation(ReservationViewModel model, bool isNew);


        ReservationViewModel ToReservationViewModel(Reservation reservation);


        ComplaintViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);
    }
}
