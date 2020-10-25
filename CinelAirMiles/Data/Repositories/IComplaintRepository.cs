namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IComplaintRepository : IGenericRepository<ClientComplaint>
    {
        IEnumerable<SelectListItem> GetComboComplaintTypes();


        /// <summary>
        ///  get the attributes of the first client, where the name is the same as the one received
        /// </summary>
        /// <param name="identityName">identity Name</param>
        /// <returns>attributes of the first client, where the name is the same as the one received
        Task<ClientComplaint> GetFirstClientAsync(string identityName);



        /// <summary>
        /// get the attributes of the clients, including the user who created it, for the client ID received
        /// </summary>
        /// <param name="clientId">Id client</param>
        /// <returns>attributes of the clients, including the user who created it, for the client ID received</returns>
        Task<ClientComplaint> GetClientWithUserByIdAsync(int clientId);



        /// <summary>
        /// get a list of clients, including who created
        /// </summary>
        /// <returns>list of clients</returns>
        Task<List<ClientComplaint>> GetAllClientListAsync();

    }
}
