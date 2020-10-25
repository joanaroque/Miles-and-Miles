namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IComplaintRepository : IGenericRepository<ClientComplaint>
    {
        IEnumerable<SelectListItem> GetComboComplaintTypes();

        Task<ClientComplaint> GetClientByIdAsync(int clientId);

        IQueryable GetClientComplaints(string user);
    }
}
