namespace CinelAirMilesLibrary.Common.Helpers
{
    using System;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;

    public class TransactionHelper : ITransactionHelper
    {
        private readonly IUserHelper _userHelper;

        public TransactionHelper(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public Transaction CreatePurchaseTransaction(User user, PremiumOffer offer)
        {
            var transaction = new Transaction
            {
                StartBalance = user.BonusMiles,
                CreatedBy = user,
                CreateDate = DateTime.UtcNow,
                TransactionType = Enums.TransactionType.Debit,
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
                TransactionType = Enums.TransactionType.Credit,
                Value = reservation.MyPremium.Price,
                Product = reservation.MyPremium,
                Status = 0
            };
            transaction.EndBalance = transaction.StartBalance + transaction.Value;

            return transaction;
        }


        public int MilesPrice(int miles)
        {
            var price = miles * 0.05;

            return (int)price;
        }



        public Transaction NewPurchase(Transaction transaction, User user)
        {
            var quantity = transaction.Value;

            var price = MilesPrice(quantity);

            var endBalance = transaction.StartBalance + quantity;

            transaction = new Transaction
            {
                Value = quantity,
                Price = price,
                StartBalance = transaction.User.StatusMiles,
                EndBalance = endBalance,
                CreateDate = DateTime.UtcNow,
                CreatedBy = user
            };

            return transaction;

        }


        public Transaction NewTransfer(Transaction transaction, User user, User userTo)
        {
            var quantity = transaction.Value;

            var endBalance = transaction.StartBalance - quantity;

            transaction = new Transaction
            {
                Value = quantity,
                Price = 10,
                StartBalance = transaction.User.StatusMiles,
                EndBalance = endBalance,
                TransferTo = userTo,
                CreateDate = DateTime.UtcNow,
                CreatedBy = user
            };

            return transaction;

        }
    }
}
