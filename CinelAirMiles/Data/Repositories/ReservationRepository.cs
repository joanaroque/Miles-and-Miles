﻿namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly DataContextClients _context;

        public ReservationRepository(DataContextClients context) : base(context)
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
