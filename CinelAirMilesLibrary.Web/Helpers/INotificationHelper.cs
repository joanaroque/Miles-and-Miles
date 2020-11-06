using System.Collections.Generic;
using System.Threading.Tasks;

using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Enums;

namespace CinelAirMilesLibrary.Common.Helpers
{
    public interface INotificationHelper
    {
        /// <summary>
        /// Creates a Notification that is stored in Context and sends a real-time notification to all Users connected 
        /// </summary>
        /// <param name="id">Id from the item that was created</param>
        /// <param name="type">The type of User that created the notification</param>
        /// <param name="message">a message attachment</param>
        /// <returns></returns>
        Task CreateNotification(string id, UserType type, string message);
    }
}
