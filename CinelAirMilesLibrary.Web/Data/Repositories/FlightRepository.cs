namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly DataContext _context;


        public FlightRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<Flight>> GetAllFlightListAsync()
        {
            var flight = await _context.Flights
                .Include(t => t.Partner).ToListAsync();

            return flight;
        }

        public async Task<Flight> GetByIdWithIncludesAsync(int id)
        {
            var flight = await _context.Flights
                .Include(f => f.CreatedBy)
                .Where(f => f.Id.Equals(id)).FirstOrDefaultAsync();

            return flight;
        }


        public async Task<IEnumerable<Flight>> GetFlightsByPartner(int id)
        {
            return await _context.Flights
                .Include(p => p.Partner)
                .Where(pi => pi.Partner.Id == id)
                .ToListAsync();
        }

        public IEnumerable<SelectListItem> GetComboFlightList()
        {
            var list = _context.Flights.Where(dd => dd.DepartureDate > DateTime.UtcNow);

            var selectList = list.Select(fl => new SelectListItem
            {
                Value = fl.Id.ToString(),
                Text = fl.Origin + "->" + fl.Destination
            });

            return selectList;
        }
    }
}
