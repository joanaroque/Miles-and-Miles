using System.Collections.Generic;
using System.Threading.Tasks;

using CinelAirMilesLibrary.Common.Enums;

namespace CinelAirMilesLibrary.Common.Helpers
{
    public interface INotificationHelper
    {
        /// <summary>
        /// Creates a Notification that is stored in Context and sends a real-time notification to all Users connected 
        /// </summary>
        /// <param name="id">Id from the item that was created</param>
        /// <param name="type">the notification type</param>
        /// <param name="message">a message attachment</param>
        /// <param name="usergroup">the user's role</param>
        /// <returns></returns>
        Task CreateNotificationAsync(string id, UserType usergroup, string message, NotificationType type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteOldByIdAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetNotificationMessages();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usergroup"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<Response> UpdateNotificationAsync(string id, UserType usergroup, string message);
    }
}
