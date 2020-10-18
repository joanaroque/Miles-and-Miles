namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MilesBackOffice.Web.Data.Entities;

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
                 .Where(a => a.Status == 1).ToListAsync();
        }
    }
}
