namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using System.Threading.Tasks;


    public interface IReservationRepository 
    {
        /// <summary>
        /// ger reservations from the current client
        /// </summary>
        /// <param name="currentClient">current client</param>
        /// <returns>the reservation from the current client</returns>
        Task<Reservation> GetReservationFromCurrentClientByIdAsync(string currentClient);



        /// <summary>
        /// get client by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>client</returns>
        Task<Reservation> GetByIdAsync(int id);



      //  Task<bool> UpdateAsync(Reservation entity);


    }
}
