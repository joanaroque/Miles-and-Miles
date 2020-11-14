namespace CinelAirMiles.Helpers
{
    using CinelAirMiles.Models;

    using CinelAirMilesLibrary.Common.Data.Entities;


    public interface IClientConverterHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        User ToUser(RegisterNewUserViewModel model, Country country);


        //Reservation ToReservation(ReservationViewModel model, bool isNew);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="advertising"></param>
        /// <returns></returns>
        AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        ReservationViewModel ToReservationViewModel(Reservation reservation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientComplaint"></param>
        /// <returns></returns>
        ComplaintViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ClientComplaint ToClientComplaint(ComplaintViewModel model, bool isNew, User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        NotificationViewModel ToNotificationViewModel(Notification notification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        Notification ToNotification(NotificationViewModel model, bool isNew);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        TransactionViewModel ToTransactionViewModel(Transaction transaction, User user);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Transaction CreateConversionTransaction(TransactionViewModel model, User user);
        Transaction CreateTransferTransaction(TransactionViewModel model, User from, User to);
        Transaction CreatePurchaseTransaction(TransactionViewModel model, User user);
    }
}
