namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface ITierChangeRepository : IGenericRepository<TierChange>
    {

        /// <summary>
        /// get a list of clients, including who created
        /// </summary>
        /// <returns>list of clients</returns>
        Task<List<TierChange>> GetAllClientListAsync();



        /// <summary>
        /// get the first or default tier change
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>a tier change</returns>
        Task<TierChange> GetByIdWithIncludesAsync(int id);
    }
}
