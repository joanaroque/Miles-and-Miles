using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class AvailableSeatsViewModel
    {
        public string Id { get; set; }


        [Required]
        public string Name { get; set; }



        public int FlightNumber { get; set; }



        public int NumberOfSeats { get; set; }



        public int AvailableSeats { get; set; }



        public bool IsConfirm { get; set; }
    }
}
