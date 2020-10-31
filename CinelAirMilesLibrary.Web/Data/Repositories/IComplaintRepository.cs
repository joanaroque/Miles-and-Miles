using CinelAirMilesLibrary.Common.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    public interface IComplaintRepository : IGenericRepository<ClientComplaint>
    {
        IEnumerable<SelectListItem> GetComboComplaintTypes();

        Task<ClientComplaint> GetClientByIdAsync(int clientId);

        Task<List<ClientComplaint>> GetClientComplaintsAsync(string user);

        /// <summary>
        /// get the first or default client complaint, including who created
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>a client complaint</returns>
        Task<ClientComplaint> GetByIdWithIncludesAsync(int id);


        /// <summary>
        /// get a list of client complaint that includes who created
        /// </summary>
        /// <returns>list of client complaint</returns>
        Task<List<ClientComplaint>> GetAllComplaintsAsync();

    }
}
