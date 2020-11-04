using System;
using System.Threading.Tasks;

namespace CinelAirMilesMySQL.DataBase
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;

        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            await FillFlightsAsync();

        }

        private async Task FillFlightsAsync()
        {

            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123456789",
                FlightDate = DateTime.Now.AddDays(10),
                Departure = "Brasil",
                Arrival = "Suécia",
                Tariff = "Desconto"

            });


            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123450789",
                FlightDate = DateTime.Now.AddDays(11),
                Departure = "Alemanha",
                Arrival = "México",
                Tariff = "Básica"

            });


            _context.Tickets.Add(new Ticket
            {

                ClientNumber = "123456189",
                FlightDate = DateTime.Now.AddDays(12),
                Departure = "Noruega",
                Arrival = "Costa Rica",
                Tariff = "Clássica"
            });


            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "121456789",
                FlightDate = DateTime.Now.AddDays(13),
                Departure = "Espanha",
                Arrival = "Finlândia",
                Tariff = "Desconto"

            });


            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123456489",
                FlightDate = DateTime.Now.AddDays(14),
                Departure = "Natal",
                Arrival = "Lisboa",
                Tariff = "Clássica"

            });


            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123455789",
                FlightDate = DateTime.Now.AddDays(15),
                Departure = "Madrid",
                Arrival = "Oslo",
                Tariff = "Básica"

            });

            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123456189",
                FlightDate = DateTime.Now.AddDays(16),
                Departure = "Faro",
                Arrival = "Madeira",
                Tariff = "Básica"

            });

            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123456889",
                FlightDate = DateTime.Now.AddDays(17),
                Departure = "Marte",
                Arrival = "Jupiter",
                Tariff = "Clássica"

            });

            _context.Tickets.Add(new Ticket
            {
                ClientNumber = "123456729",
                FlightDate = DateTime.Now.AddDays(18),
                Departure = "Inglaterra",
                Arrival = "Russia",
                Tariff = "Desconto"
            });


            await _context.SaveChangesAsync();

        }
    }
}
