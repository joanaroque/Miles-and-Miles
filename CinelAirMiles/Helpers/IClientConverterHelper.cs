namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;

    using CinelAirMilesLibrary.Common.Data.Entities;


    public interface IClientConverterHelper
    {
        //Reservation ToReservation(ReservationViewModel model, bool isNew);

        AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising);



        ReservationViewModel ToReservationViewModel(Reservation reservation);


        ComplaintViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);


        ClientComplaint ToClientComplaint(ComplaintViewModel model, bool isNew);


        NotificationViewModel ToNotificationViewModel(Notification notification);


        Notification ToNotification(NotificationViewModel model, bool isNew);


        TransactionViewModel ToTransactionViewModel(Transaction transaction, User user);
    }
}
