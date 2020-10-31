namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Collections.Generic;
    using System.Linq;

    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public List<Notification> GetAllNotifications(string currentClient)
        {
            var client = _context.Notifications
              .Where(n => n.CreatedBy.Id == currentClient)
              .ToList();

            return client;
        }


        public List<Notification> GetUnreadNotifications(string clientId)
        {
            var noti = _context.Notifications
                .Where(n => n.CreatedBy.Id == clientId).ToList();


            return noti;
        }
    }
}
