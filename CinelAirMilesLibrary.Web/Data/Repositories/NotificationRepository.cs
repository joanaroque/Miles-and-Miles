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

        public IEnumerable<Notification> GetAllNotifications(string currentClient)
        {
            var client = _context.Notifications
              .Where(n => n.CreatedBy.Id == currentClient)
              .ToList();

            return client;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByRoleAsync(UserType role)
        {
            return await _context.Notifications
            .Where(t => t.UserGroup == role && t.Status == 1).AsNoTracking().ToListAsync();
        }

        public async Task<Notification> GetUnreadNotifications(int clientId)
        {
            var noti = await _context.Notifications
                .Where(n => n.Id.Equals(clientId) && n.Status == 1)
                .FirstOrDefaultAsync();

            return noti;
        }

        public async Task<int> GetAdminLenght(UserType role)
        {
            return await _context.Notifications
                .Where(u => u.UserGroup == role
                && u.Type == NotificationType.Recover
                || u.Type == NotificationType.NewClient).CountAsync();
        }


        public async Task<int> GetPremiumLenghtByRole(UserType role)
        {
            return await _context.Notifications
                .Where(u => u.UserGroup == role
                && (u.Type == NotificationType.Ticket
                || u.Type == NotificationType.Upgrade
                || u.Type == NotificationType.Voucher))
                .CountAsync();
        }

        public async Task<int> GetPartnerLenghtByRole(UserType role)
        {
            return await _context.Notifications
                .Where(u => u.UserGroup == role
                && u.Type == NotificationType.Partner)
                .CountAsync();
        }

        public async Task<int> GetAdvertLenghtByRole(UserType role)
        {
            return await _context.Notifications
                .Where(u => u.UserGroup == role
                && u.Type == NotificationType.Advertising)
                .CountAsync();
        }

        public async Task<Notification> GetByGuidIdAsync(string id)
        {
            return await _context.Notifications
                .Where(i => i.ItemId == id).FirstOrDefaultAsync();
        }

        public async Task<Notification> GetByGuidIdAndTypeAsync(string id, NotificationType recover)
        {
            return await _context.Notifications
                .Where(u => u.ItemId == id && u.Type.Equals(recover))
                .FirstOrDefaultAsync();
        }

        public bool ExistsNotificationByGuid(string guidId)
        {
            return _context.Notifications
                .Any(f => f.ItemId == guidId);
        }
    }
}
