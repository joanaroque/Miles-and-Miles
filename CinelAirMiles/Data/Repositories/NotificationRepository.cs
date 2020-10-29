namespace CinelAirMiles.Data.Repositories
{

    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
              .Where(n => n.CreatedBy.Id == currentClient)
              .ToList();

            return client;
        }

        public async Task<Notification> GetByIdWithIncludesAsync(int id)
        {
            var notification = await _context.Notifications
                .Include(t => t.CreatedBy)
                .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return notification;
        }

        public List<Notification> GetUnreadNotifications(string clientId)
        {
            var noti = _context.Notifications
                .Where(n => n.CreatedBy.Id == clientId && n.Status == 8)
                .ToList();


            return noti;
        }
    }
}
