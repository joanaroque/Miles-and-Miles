using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    public class ClientComplaintRepository : GenericRepository<ClientComplaint>, IClientComplaintRepository
    {
        private readonly DataContext _context;


        public ClientComplaintRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<ClientComplaint> GetByIdWithIncludesAsync(int id)
        {
            var complaint = await _context.ClientComplaints
                  .Include(t => t.CreatedBy)
                  .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return complaint;
        }

        public async Task<List<ClientComplaint>> GetClientComplaintsAsync()
        {
            return await _context.ClientComplaints
               // .Include(c => c.CreatedBy)  
                .Where(c => c.PendingComplaint == false).ToListAsync();
        }
    }
}
