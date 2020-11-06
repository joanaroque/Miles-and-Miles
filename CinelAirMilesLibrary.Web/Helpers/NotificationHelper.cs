using System;
using System.Collections.Generic;
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

        public async Task CreateNotification(string id, UserType type, string message)
        {
            var notification = new Notification
            {
                Message = message ?? null,
                Status = 1,
                UserGroup = type,
                ItemId = id?? null,
                CreateDate = DateTime.UtcNow
            };
            await _notificationRepository.CreateAsync(notification);

            await _hubContext.Clients.All.DbChangeNotification();
        }

    }
}
