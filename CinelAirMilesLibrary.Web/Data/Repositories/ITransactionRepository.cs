namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<Response> AddTransanctionAsync(Transaction trans);
    }
}
