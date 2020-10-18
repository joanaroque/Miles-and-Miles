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
                 .Include(a => a.CreatedBy)
                 .Include(a => a.Title)
                 .Include(a => a.Content)
                 .Include(a => a.EndDate)
                 .Where(a => a.IsConfirm == false).ToListAsync();
        }
    }
}
