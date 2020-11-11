using CinelAirMilesLibrary.Common.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    public interface IComplaintRepository : IGenericRepository<ClientComplaint>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboComplaintTypes();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<ClientComplaint> GetClientByIdAsync(int clientId);


        /// <summary>
        /// Get all entries for a specific User
        /// </summary>
        /// <param name="id">Id of the user that created the complaint</param>
        /// <returns>A list of complaints</returns>
        Task<List<ClientComplaint>> GetClientComplaintsAsync(string id);



        /// <summary>
        /// get a list of client complaint that includes who created
        /// </summary>
        /// <returns>list of client complaint</returns>
        Task<List<ClientComplaint>> GetAllComplaintsAsync();

    }
}
