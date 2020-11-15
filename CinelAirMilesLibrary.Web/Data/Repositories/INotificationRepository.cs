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
        IEnumerable<Notification> GetAllNotifications(string currentClient);


        /// <summary>
        /// Get the count of a list of notifications filtered by user role
        /// and by Type PremiumOffer
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<int> GetPremiumLenghtByRole(UserType role);


        /// <summary>
        /// Get a list of notifications by user role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotificationsByRoleAsync(UserType role);


        /// <summary>
        /// get list of only unread notifications
        /// </summary>
        /// <param name="clientId">client Id</param>
        /// <returns>list of unread notifications</returns>
        Task<Notification> GetUnreadNotifications(int clientId);

        /// <summary>
        /// Get the count of a list of notifications filtered by user role
        /// and by Type Partner
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<int> GetPartnerLenghtByRole(UserType role);

        /// <summary>
        /// Get the count of a list of notifications filtered by user role
        /// and by Type Advertising
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<int> GetAdvertLenghtByRole(UserType role);

        /// <summary>
        /// Get a notification by the Guid Id of the item it's attached to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Notification> GetByGuidIdAsync(string id);

        /// <summary>
        /// Get the lenght of notifications for administrator
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<int> GetAdminLenght(UserType role);

        /// <summary>
        /// gets a notification by user and type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recover"></param>
        /// <returns></returns>
        Task<Notification> GetByGuidIdAndTypeAsync(string id, NotificationType recover);
    }
}
