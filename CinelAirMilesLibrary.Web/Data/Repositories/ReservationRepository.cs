namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Reservation>> GetReservationsFromCurrentClientToListAsync(string clientId)
        {
            var clientReservation = await _context.Reservations
                .Include(r => r.CreatedBy)
                .Where(r => r.CreatedBy.Id == clientId)
               .ToListAsync();

            return clientReservation;
        }
    }
}
