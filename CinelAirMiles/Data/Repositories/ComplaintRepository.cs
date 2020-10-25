namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMiles.Helpers;
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
        private readonly DataContextClients _context;

        public ComplaintRepository(
            DataContextClients context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientComplaint> GetClientByIdAsync(int clientId)
        {
            var client = await _context.ClientComplaints
                   .Include(o => o.CreatedBy)
                    .FirstOrDefaultAsync(o => o.Id == clientId);

            return client;
        }

        public IQueryable GetClientComplaints(string user)
        {
            var complaints = _context.ClientComplaints
                .OrderBy(c => c.CreateDate)
                .Where(c => c.CreatedBy.Email == user);

            return complaints;
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
