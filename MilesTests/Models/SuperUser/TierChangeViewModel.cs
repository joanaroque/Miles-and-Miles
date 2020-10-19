namespace MilesBackOffice.Web.Models.SuperUser
{
    using MilesBackOffice.Web.Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class TierChangeViewModel
    {

        public int TierChangeId { get; set; }


        public string OldTier { get; set; }



        public string NewTier { get; set; }



        public bool IsConfirm { get; set; }




        public int NumberOfFlights { get; set; }



        public long NumberOfMiles { get; set; }



        [Required]
        public User Client { get; set; }


        public string Observations { get; set; }

    }
}
