using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class TierChangeViewModel
    {

        public string Id { get; set; }


        [Required]
        public string Name { get; set; }



        public string OldTier { get; set; }



        public string NewTier { get; set; }



        public bool IsConfirm { get; set; }



        public int NumberOfFlights { get; set; }



        public long NumberOfMiles { get; set; }

    }
}
