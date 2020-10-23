namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClientComplaintRepository : IGenericRepository<ClientComplaint>
    {
        /// <summary>
        /// get a list of client complaint that includes who created
        /// </summary>
        /// <returns>list of client complaint</returns>
        Task<List<ClientComplaint>> GetClientComplaintsAsync();


        /// <summary>
        /// get the first or default client complaint, including who created
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>a client complaint</returns>
        Task<ClientComplaint> GetByIdWithIncludesAsync(int id);

    }
}
