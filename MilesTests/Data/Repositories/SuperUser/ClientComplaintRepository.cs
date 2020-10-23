﻿namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


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
                  .Include(c => c.CreatedBy)
                  .Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();

            return complaint;
        }

        public async Task<List<ClientComplaint>> GetClientComplaintsAsync()
        {
            var complaint = await _context.ClientComplaints
                .Include(c => c.CreatedBy).ToListAsync();

            return complaint;
        }
    }
}
