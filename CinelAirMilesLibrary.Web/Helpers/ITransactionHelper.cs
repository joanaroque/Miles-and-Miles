using System;
using System.Collections.Generic;
using System.Text;

using CinelAirMilesLibrary.Common.Data.Entities;

namespace CinelAirMilesLibrary.Common.Helpers
{
    public interface ITransactionHelper
    {
        /// <summary>
        /// Creates a Transaction for a cancelled reservation
        /// </summary>
        /// <param name="reservation">Reservation model including User and PremiumOffer</param>
        /// <returns>The Transaction created</returns>
        Transaction CreateCancelPurchaseTransaction(Reservation reservation);


        /// <summary>
        /// Creates a Transaction for a PremiumOffer Purchase
        /// </summary>
        /// <param name="user">Current User</param>
        /// <param name="offer">PremiumOffer</param>
        /// <returns>The Transaction created</returns>
        Transaction CreatePurchaseTransaction(User user, PremiumOffer offer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Transaction NewPurchase(Transaction transaction, User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="miles"></param>
        /// <returns></returns>
        int MilesPrice(int miles);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="user"></param>
        /// <param name="userTo"></param>
        /// <returns></returns>
        Transaction NewTransfer(Transaction transaction);
    }
}
