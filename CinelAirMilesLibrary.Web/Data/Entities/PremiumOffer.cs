namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PremiumOffer : IEntity
    {
        public string Title { get; set; }


        public Flight Flight { get; set; }


        public Partner Partner { get; set; }


        public int Quantity { get; set; }


        public int Price { get; set; }


        public string OfferIdGuid { get; set; }


        public byte[] Image { get; set; }
        /// <summary>
        /// for vouchers
        /// </summary>
        public string Conditions { get; set; }


        public PremiumType Type { get; set; }


        /************OBJECT PROPERTIES****************************/

        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }

        /// <summary>
        /// 0 - Approved by a SU
        /// 1 - Waiting approval by a SU
        /// 2 - Returned by a SU for editing
        /// 3 - To be deleted
        /// </summary>
        public int Status { get; set; }
    }
}
