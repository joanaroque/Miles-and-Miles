namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.Enums;
    using System;

    public class PremiumOffer : IEntity
    {
        public string Title { get; set; }


        public string Flight { get; set; }


        public Partner Partner { get; set; }


        public int Quantity { get; set; }


        public int Price { get; set; }


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


        public int Status { get; set; }
    }
}
