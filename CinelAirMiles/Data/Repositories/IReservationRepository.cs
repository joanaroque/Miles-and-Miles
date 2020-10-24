namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Threading.Tasks;


    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        /// <summary>
        /// ger reservations from the current client
        /// </summary>
        /// <param name="currentClient">current client</param>
        /// <returns>the reservation from the current client</returns>
        Task<Reservation> GetCurrentClientByIdAsync(string currentClient);




    }
}
