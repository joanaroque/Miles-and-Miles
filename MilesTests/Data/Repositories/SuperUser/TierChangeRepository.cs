namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using Microsoft.EntityFrameworkCore;

    using MilesBackOffice.Web.Data.Entities;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class TierChangeRepository : GenericRepository<TierChange>, ITierChangeRepository
    {
        private readonly DataContext _context;

        public TierChangeRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<TierChange> GetByIdWithIncludesAsync(int id)
        {
            var tierChange = await _context.TierChanges
                 .Include(t => t.Client)
                 .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return tierChange;
        }

        public async Task<List<TierChange>> GetAllClientListAsync()
        {
            return await _context.TierChanges.ToListAsync();
        }
    }
}
