using System;

namespace CinelAirMilesMySQL.DataBase
{
    public partial class Ticket
    {
        public int Id { get; set; }


        public string ClientNumber { get; set; }


        public DateTime? FlightDate { get; set; }


        public string Departure { get; set; }


        public string Arrival { get; set; }


        public string Tariff { get; set; }
    }
}
