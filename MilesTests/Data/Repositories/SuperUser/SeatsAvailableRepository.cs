using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    public class SeatsAvailableRepository : GenericRepository<SeatsAvailable>, ISeatsAvailableRepository
    {
        private readonly DataContext _context;


        public SeatsAvailableRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<SeatsAvailable>> GetSeatsToBeConfirmAsync()
        {
            return await _context.SeatsAvailables
                .Include(s => s.CreatedBy)
                .Include(s => s.FlightNumber)
                .Include(s => s.MaximumSeats)
                .Include(s => s.AvailableSeats)
                .Where(s => s.PendingSeatsAvailable == false).ToListAsync();
        }
    }
}
