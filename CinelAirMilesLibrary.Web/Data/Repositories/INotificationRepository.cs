namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;

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
        IEnumerable<Notification> GetNotificationsByRole(UserType role);




        /// <summary>
        /// get list of only unread notifications
        /// </summary>
        /// <param name="clientId">client Id</param>
        /// <returns>list of unread notifications</returns>
        Task<Notification> GetUnreadNotifications(int clientId);

    }
}
