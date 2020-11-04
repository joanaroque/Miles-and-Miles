namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        /// <summary>
        /// get a list of reservations from the current client
        /// </summary>
        /// <param name="currentClient">current client</param>
        /// <returns> reservation list from the current client</returns>
        Task<List<Reservation>> GetReservationsFromCurrentClientToListAsync(string clientId);

        /// <summary>
        /// gets a reservation by its Id including user details and premiumoffer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Reservation> GetByIdIncludingAsync(int id);
        Task<Response> UpdateReservationAsync(Reservation model);
    }
}
