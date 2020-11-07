namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;

    public class Reservation : IEntity
    {
        public string ReservationID { get; set; }


        public PremiumOffer MyPremium { get; set; }

        /********************/
        public int Id { get; set; }



        public User CreatedBy { get; set; }



        public DateTime CreateDate { get; set; }



        public DateTime UpdateDate { get; set; }



        public User ModifiedBy { get; set; }

        /// <summary>
        /// Status:
        /// 0 - Confirmed
        /// 1 - Pending
        /// 2 - Denied
        /// </summary>
        public int Status { get; set; }
    }
}
