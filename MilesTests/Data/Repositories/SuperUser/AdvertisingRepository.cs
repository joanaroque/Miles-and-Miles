namespace MilesBackOffice.Web.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Data.Repositories.SuperUser;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class AdvertisingRepository : GenericRepository<Advertising>, IAdvertisingRepository
    {
        private readonly DataContext _context;


        public AdvertisingRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<Advertising>> GetAllAdvertisingAsync()
        {
            var advertising = await _context.Advertisings
                 .Include(a => a.Partner).ToListAsync();

            return advertising;

        }

        public async Task<Advertising> GetByIdWithIncludesAsync(int id)
        {
            var advertising = await _context.Advertisings
                 .Include(a => a.CreatedBy)
                 .Include(a => a.Partner)
                 .Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();

            return advertising;
        }
    }
}
