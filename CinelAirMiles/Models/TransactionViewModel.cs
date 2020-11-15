namespace CinelAirMiles.Models
{
    using System.Collections.Generic;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TransactionViewModel
    {
        public int Id { get; set; }


        public string UserGuidID { get; set; }



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


        public int PremiumOffer { get; set; }


        public IEnumerable<SelectListItem> Values { get; set; }
    }
}
