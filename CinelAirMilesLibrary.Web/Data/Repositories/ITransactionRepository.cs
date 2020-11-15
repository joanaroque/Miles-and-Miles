namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<Response> AddTransanctionAsync(Transaction trans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int GetStatusMiles(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int GetBonusMiles(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Transaction>> GetAllByClient(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Transaction>> GetByClientIdAsync(string id);


        /// <summary>
        /// Get the total of transaction values from the current season
        /// </summary>
        /// <param name="user"></param>
        /// <returns>An int that reflects the total amount of transaction values</returns>
        int GetTransactionHistory(User user);
    }
}
