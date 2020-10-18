using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class AdvertisingRepository : GenericRepository<Advertising>, IAdvertisingRepository
    {
        private readonly DataContext _context;


        public AdvertisingRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<Advertising>> GetAdvertisingToBeConfirmAsync()
        {
            return await _context.Advertisings
                // .Include(a => a.CreatedBy)
                 .Where(a => a.PendingPublish == false).ToListAsync();
        }

        public async Task<Advertising> GetByIdWithIncludesAsync(int id)
        {
            var advertising = await _context.Advertisings
                 .Include(t => t.CreatedBy)
                 .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return advertising;
        }
    }
}
