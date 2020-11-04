namespace CinelAirMilesLibrary.Common.Helpers
{
    using System;

    using CinelAirMilesLibrary.Common.Data.Entities;

    public class TransactionHelper : ITransactionHelper
    {
        public Transaction CreatePurchaseTransaction(User user, PremiumOffer offer)
        {
            var transaction = new Transaction
            {
                StartBalance = user.BonusMiles,
                CreatedBy = user,
                CreateDate = DateTime.UtcNow,
                Type = Enums.TransactionType.Debit,
                Value = offer.Price,
                Product = offer,
                Status = 0
            };
            transaction.EndBalance = transaction.StartBalance - transaction.Value;

            return transaction;
        }


        public Transaction CreateCancelPurchaseTransaction(Reservation reservation)
        {
            var transaction = new Transaction
            {
                StartBalance = reservation.CreatedBy.BonusMiles,
                CreatedBy = reservation.CreatedBy,
                CreateDate = DateTime.UtcNow,
                Type = Enums.TransactionType.Credit,
                Value = reservation.MyPremium.Price,
                Product = reservation.MyPremium,
                Status = 0
            };
            transaction.EndBalance = transaction.StartBalance + transaction.Value;

            return transaction;
        }
    }
}
