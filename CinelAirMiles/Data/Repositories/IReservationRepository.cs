namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        /// <summary>
        /// ger a list of reservations from the current client
        /// </summary>
        /// <param name="currentClient">current client</param>
        /// <returns> reservation list from the current client</returns>
        Task<List<Reservation>> GetReservationsFromCurrentClientToListAsync();

    }
}
