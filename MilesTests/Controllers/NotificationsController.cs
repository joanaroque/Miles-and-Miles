namespace MilesBackOffice.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CinelAirMilesLibrary.Common.Helpers;
    using System.Threading.Tasks;
    using CinelAirMilesLibrary.Common.Data.Repositories;

    public class NotificationsController : Controller
    {
        private readonly INotificationHelper _notificationHelper;
        private readonly IUserHelper _userHelper;
        private readonly INotificationRepository _notificationRepository;

        public NotificationsController(INotificationHelper notificationHelper,
            IUserHelper userHelper,
            INotificationRepository notificationRepository)
        {
            _notificationHelper = notificationHelper;
            _userHelper = userHelper;
            _notificationRepository = notificationRepository;
        }


        public async Task<IActionResult> GetNotifications()
        {
            var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
            if (user == null)
            {
                return Json(null);
            }
            return Json(_notificationRepository.GetNotificationsByRole(user.SelectedRole));
        }


    }
}
