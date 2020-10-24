using System;

namespace CinelAirMiles.Models
{
    public class ReservationViewModel
    {

        public int ReservationId { get; set; }



        public string ClientName { get; set; }



        public string Destination { get; set; }



        public string PartnerName { get; set; }


        public DateTime Date { get; set; }


        public int Status { get; set; }


    }
}
