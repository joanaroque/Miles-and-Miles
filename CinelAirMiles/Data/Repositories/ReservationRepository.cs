namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;


    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContextClients _context;

        public ReservationRepository(DataContextClients context)
        {
            _context = context;
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var client = await _context.Reservations
                .FirstOrDefaultAsync(c => c.Id == id);

            return client;
        }

        public async Task<Reservation> GetReservationFromCurrentClientByIdAsync(string currentClient)
        {
            var clientReservation = await _context.Reservations
               .Include(r => r.CreatedBy)
               .Where(r => r.CreatedBy.Id == currentClient.ToString())
               .FirstOrDefaultAsync();

            return clientReservation;
        }

        //public async Task<bool> UpdateAsync(Reservation entity)
        //{
        //    _context.Reservations.Update(entity);

        //    return await Save
        //}
    }
}
