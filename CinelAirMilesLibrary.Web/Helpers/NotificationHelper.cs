using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Enums;

namespace CinelAirMilesLibrary.Common.Helpers
{
    public class NotificationHelper : INotificationHelper
    {
        //private readonly IHubContext<NotificationHub, INotify> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public NotificationHelper(INotificationRepository notificationRepository)
        {
            //_hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task CreateNotificationAsync(string id, UserType usergroup, string message, NotificationType type)
        {
            var notification = new Notification
            {
                Message = message ?? null,
                Status = 1,
                UserGroup = usergroup,
                ItemId = id ?? null,
                CreateDate = DateTime.UtcNow,
                Type = type
            };
            await _notificationRepository.CreateAsync(notification);

            //await _hubContext.Clients.All.DbChangeNotification();
        }


        public async Task DeleteOldByIdAsync(string id)
        {
            var notification = await _notificationRepository.GetByGuidIdAsync(id);

            if (notification != null)
            {
                await _notificationRepository.DeleteAsync(notification);//or status 0
            }
        }


        public async Task<Response> UpdateNotificationAsync(string id, UserType usergroup, string message)
        {
            var notification = await _notificationRepository.GetByGuidIdAsync(id);
            if (notification != null)
            {
                notification.UserGroup = usergroup;
                notification.Message = message ?? null;

                await _notificationRepository.UpdateAsync(notification);

                return new Response
                {
                    Success = true
                };
            }

            return new Response
            {
                Success = false
            };
        }

        public IEnumerable<string> GetNotificationMessages()
        {
            var messages = new List<string>
            {
                "EDIT - Incorrect Title",
                "EDIT - Not enough seats available",
                "EDIT - Create more availability",
                "DELETE - No seats available",
                "DELETE - Not relevant"
            };

            return messages;
        }
    }
}
