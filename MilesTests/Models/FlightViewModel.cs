using System;

namespace MilesBackOffice.Web.Models
{
    public class FlightViewModel
    {

        public int FlightId { get; set; }



        public string Origin { get; set; }



        public string Destination { get; set; }


        public DateTime DepartureDate { get; set; }

        public int MaximumSeats { get; set; }



        public int AvailableSeats { get; set; }



        public string PartnerName { get; set; }



        public int Miles { get; set; }



        public int Status { get; set; }

    }
}
