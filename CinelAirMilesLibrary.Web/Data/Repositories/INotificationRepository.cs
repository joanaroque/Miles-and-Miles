namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationRepository : IGenericRepository<Notification>
    {
        /// <summary>
        /// ger list of all notifications
        /// </summary>
        /// <param name="currentClient">current client</param>
        /// <returns>list notifications</returns>
        List<Notification> GetAllNotifications(string currentClient);




        /// <summary>
        /// get list of only unread notifications
        /// </summary>
        /// <param name="clientId">client Id</param>
        /// <returns>list of unread notifications</returns>
        List<Notification> GetUnreadNotifications(string clientId);

    }
}
