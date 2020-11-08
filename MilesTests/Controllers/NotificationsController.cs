namespace MilesBackOffice.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CinelAirMilesLibrary.Common.Helpers;
    using System.Threading.Tasks;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using System.Linq;
    using CinelAirMilesLibrary.Common.Enums;
    using System.Collections.Generic;

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
            var list = _premiumRepository.GetAll();
            if (user.SelectedRole == UserType.SuperUser)
            {
                list = list.Where(st => st.Status == 1);
            }
            else if (user.SelectedRole == UserType.User)
            {
                list = list.Where(st => st.Status == 2);
            }

            return Json(list.Count());
        }

        
        [HttpGet]
        public async Task<IActionResult> GetPartnerNotifications()
        {
            var user = await GetUserByUserNameAsync();
            if (user == null)
            {
                return NotFound();
            }
            var list = _partnerRepository.GetAll();
            if (user.SelectedRole == UserType.SuperUser)
            {
                list = list.Where(st => st.Status == 1);
            }
            else if (user.SelectedRole == UserType.User)
            {
                list = list.Where(st => st.Status == 2);
            }

            return Json(list.Count());
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisingNotifications()
        {
            var user = await GetUserByUserNameAsync();
            if (user == null)
            {
                return NotFound();
            }
            var list = _advertisingRepository.GetAll();
            if (user.SelectedRole == UserType.SuperUser)
            {
                list = list.Where(st => st.Status == 1);
            }
            else if (user.SelectedRole == UserType.User)
            {
                list = list.Where(st => st.Status == 2);
            }

            return Json(list.Count());
        }

        private async Task<User> GetUserByUserNameAsync()
        {
            return await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
        }
    }
}
