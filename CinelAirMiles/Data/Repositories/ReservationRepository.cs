namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;


    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly DataContextClients _context;

        public ReservationRepository(DataContextClients context) : base(context)
        {
            _context = context;
        }


        public async Task<Reservation> GetCurrentClientByIdAsync(string currentClient)
        {
            var clientReservation = await _context.Reservations
               .Where(r => r.CreatedBy.Id == currentClient.ToString())
               .FirstOrDefaultAsync();

            return clientReservation;
        }
    }
}
