namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.Enums;

    using System;


    public class TierChange : IEntity
    {

        public TierType OldTier { get; set; }


        public TierType NewTier { get; set; }


        public int NumberOfFlights { get; set; }


        public int NumberOfMiles { get; set; }



        public User Client { get; set; }

        /// <summary>
        /// Status:
        /// 0 - Confirmed
        /// 1 - Pending
        /// 2 - Denied (why?)
        /// </summary>
        public int Status { get; set; }


        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }



    }
}
