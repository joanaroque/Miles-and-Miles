namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;

    using CinelAirMilesLibrary.Common.Enums;

    public class Transaction : IEntity
    {
        public int Id { get; set; }


        public int StartBalance { get; set; }

        public int EndBalance { get; set; }

        /// <summary>
        /// In miles
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 0 - Debit,
        /// 1 - Credit,
        /// 2 - Loan,
        /// 3 - Purchase,
        /// 4 - Transfer,
        /// 5 - Extension
        /// </summary>
        public TransactionType Type { get; set; }

        public User TransferTo { get; set; }

        /// <summary>
        /// Price of the transaction
        /// Applicable in:
        /// - Purchase of miles
        /// - Transfer
        /// - Extend
        /// - Conversion
        /// 
        /// In Euros
        /// </summary>
        public decimal Price { get; set; }


        public PremiumOffer Product { get; set; }


        /**************IEntity Props*********************/
        
        /// <summary>
        /// Transaction is from this User 
        /// </summary>
        public User CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        /// can be updated if the transaction is reverted
        /// or denied by an Admin
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// the user responsible for cancelling/denying the transaction
        /// it'll show the client in case of reverting
        /// shows the admin who denied the transaction
        /// </summary>
        public User ModifiedBy { get; set; }

        /// <summary>
        /// 0 - OK
        /// 1 - Pending Transaction
        /// 2 - Reverted
        /// 3 - Denied
        /// </summary>
        public int Status { get; set; }
    }
}
