namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

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

        public async Task<Reservation> GetByIdIncludingAsync(int id)
        {
            return await _context.Reservations
                .Include(u => u.CreatedBy)
                .Include(p => p.MyPremium.Partner)
                .Include(o => o.MyPremium.Flight.Origin)
                 .Include(d => d.MyPremium.Flight.Destination)
                 .Include(dt => dt.CreateDate)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Reservation>> GetReservationsFromCurrentClientToListAsync(string clientId)
        {
            var clientReservation = await _context.Reservations
                .Include(r => r.CreatedBy)
                .Where(r => r.CreatedBy.Id == clientId)
               .ToListAsync();

            return clientReservation;
        }

        public async Task<Response> UpdateReservationAsync(Reservation model)
        {
            var result = await UpdateAsync(model);

            if (result)
            {
                return new Response
                {
                    Success= true,
                    Message = "You reservation was cancelled."
                };
            }
            return new Response
            {
                Success = false,
                Message = "An error ocurred while cancelling your reservation. Please try again. " +
                "If the error persists contact us through the complaints menu."
            };
        }
    }
}
