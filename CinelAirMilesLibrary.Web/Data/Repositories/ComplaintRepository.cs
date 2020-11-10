namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ComplaintRepository : GenericRepository<ClientComplaint>, IComplaintRepository
    {
        private readonly DataContext _context;

        public ComplaintRepository(
            DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ClientComplaint>> GetAllComplaintsAsync()
        {
            var complaint = await _context.ClientComplaints
                .Include(c => c.CreatedBy).ToListAsync();

            return complaint;
        }


        public async Task<ClientComplaint> GetClientByIdAsync(int clientId)
        {
            var client = await _context.ClientComplaints
                   .Include(o => o.CreatedBy)
                    .FirstOrDefaultAsync(o => o.Id == clientId);

            return client;
        }

        public async Task<List<ClientComplaint>> GetClientComplaintsAsync(string user)
        {

            var complaint = await _context.ClientComplaints
                .Include(c => c.CreatedBy)
                .Where(c => c.CreatedBy.Id == user)
                .ToListAsync();

            return complaint;
        }

        public IEnumerable<SelectListItem> GetComboComplaintTypes()
        {
            var list = Enum.GetValues(typeof(ComplaintType)).Cast<ComplaintType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            return list;
        }
    }
}
