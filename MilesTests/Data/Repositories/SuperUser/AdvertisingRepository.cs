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
            return await _context.Advertisings.ToListAsync();

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
