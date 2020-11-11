namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<Response> AddTransanctionAsync(Transaction trans);

        int GetStatusMiles(User user);

        int GetBonusMiles(User user);

        Task<List<Transaction>> GetAllByClient(User user);
        Task<IEnumerable<Transaction>> GetByClientIdAsync(string id);
    }
}
