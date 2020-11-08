namespace MilesBackOffice.Web.Controllers
{
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc;

    public class NotificationsController : Controller
    {
        private readonly INotificationHelper _notificationHelper;
        private readonly IUserHelper _userHelper;
        private readonly INotificationRepository _notificationRepository;
        private readonly IPremiumRepository _premiumRepository;
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IPartnerRepository _partnerRepository;

        public NotificationsController(INotificationHelper notificationHelper,
            IUserHelper userHelper,
            INotificationRepository notificationRepository,
            IPremiumRepository premiumRepository,
            IAdvertisingRepository advertisingRepository,
            IPartnerRepository partnerRepository)
        {
            _notificationHelper = notificationHelper;
            _userHelper = userHelper;
            _notificationRepository = notificationRepository;
            _premiumRepository = premiumRepository;
            _advertisingRepository = advertisingRepository;
            _partnerRepository = partnerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationsByRole()
        {
            var user = await GetUserByUserNameAsync();
            if (user == null)
            {
                return Json(null);
            }
            return Json(_notificationRepository.GetNotificationsByRole(user.SelectedRole));
        }


        [HttpGet]
        public async Task<IActionResult> GetPremiumNotifications()
        {
            var user = await GetUserByUserNameAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Json(await _notificationRepository.GetPremiumLenghtByRole(user.SelectedRole));
        }


        [HttpGet]
        public async Task<IActionResult> GetPartnerNotifications()
        {
            var user = await GetUserByUserNameAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Json(await _notificationRepository.GetPartnerLenghtByRole(user.SelectedRole));
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisingNotifications()
        {
            var user = await GetUserByUserNameAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Json(await _notificationRepository.GetAdvertLenghtByRole(user.SelectedRole));
        }


        private async Task<User> GetUserByUserNameAsync()
        {
            return await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
        }
    }
}
