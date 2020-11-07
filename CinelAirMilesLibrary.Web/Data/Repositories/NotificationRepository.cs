namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public IEnumerable<Notification> GetNotificationsByRole(UserType role)
        {
            var list = _context.Notifications
                .Where(t => t.UserGroup == role && t.Status == 1);

            return list;
        }

        public async Task<Notification> GetUnreadNotifications(int clientId)
        {
            var noti = await _context.Notifications
                .Where(n => n.Id.Equals(clientId) && n.Status == 1)
                .FirstOrDefaultAsync();


            return noti;
        }
    }
}
