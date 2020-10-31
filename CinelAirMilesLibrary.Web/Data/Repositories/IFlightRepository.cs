namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlightRepository : IGenericRepository<Flight>
    {
        /// <summary>
        /// get a list of flights, including who created
        /// </summary>
        /// <returns>list of flights</returns>
        Task<List<Flight>> GetAllFlightListAsync();



        /// <summary>
        /// get the first or default flights
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>a flight</returns>
        Task<Flight> GetByIdWithIncludesAsync(int id);

    }
}
