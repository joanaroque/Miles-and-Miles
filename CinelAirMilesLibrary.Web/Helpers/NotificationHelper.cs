using System;
using System.Threading.Tasks;

using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Enums;
using CinelAirMilesLibrary.Common.Hub.Notification;

using Microsoft.AspNetCore.SignalR;

namespace CinelAirMilesLibrary.Common.Helpers
{
    public class NotificationHelper : INotificationHelper
    {
        private readonly IHubContext<NotificationHub, INotify> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public NotificationHelper(IHubContext<NotificationHub, INotify> hubContext,
            INotificationRepository notificationRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task CreateNotification(string id, UserType usergroup, string message, NotificationType type)
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

            await _hubContext.Clients.All.DbChangeNotification();
        }

    }
}
