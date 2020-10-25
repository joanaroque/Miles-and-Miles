namespace CinelAirMiles.Data.Repositories
{

    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Collections.Generic;
    using System.Linq;

    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly DataContextClients _context;

        public NotificationRepository(DataContextClients context) : base(context)
        {
            _context = context;
        }


        public List<Notification> GetAllNotifications(string currentClient)
        {
            var client = _context.Notifications
              .Where(n => n.Client.Id == currentClient)
              .ToList();

            return client;
        }


        public List<Notification> GetUnreadNotifications(string clientId)
        {
            var noti = _context.Notifications
                .Where(n => n.Client.Id == clientId && !n.IsRead).ToList();


            return noti;
        }
    }
}
