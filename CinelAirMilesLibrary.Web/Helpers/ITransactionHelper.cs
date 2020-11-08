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



        int MilesPrice(int miles);
    }
}
