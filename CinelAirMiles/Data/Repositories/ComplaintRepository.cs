namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ComplaintRepository : GenericRepository<ClientComplaint>, IComplaintRepository
    {
        private readonly DataContextClients _context;

        public ComplaintRepository(DataContextClients context) : base(context)
        {
            _context = context;
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
