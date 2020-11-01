namespace MilesBackOffice.Web.Models.SuperUser
{
    using CinelAirMilesLibrary.Common.Enums;


    public class TierChangeViewModel
    {

        public int TierChangeId { get; set; }


        public TierType OldTier { get; set; }



        public TierType NewTier { get; set; }




        public int Status { get; set; }


        public int NumberOfFlights { get; set; }



        public int NumberOfMiles { get; set; }



        public string ClientName { get; set; }


        public string Observations { get; set; }

    }
}
