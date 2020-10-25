namespace CinelAirMiles.Controllers
{
    using CinelAirMiles.Data.Repositories;
    using CinelAirMiles.Helpers;
    using CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.Linq;

    public class NotificationController : Controller
    {
        private readonly IUserHelperClient _userHelper;
        private readonly INotificationRepository _notificationRepository;
        private readonly IClientConverterHelper _converterHelper;

        public NotificationController(IUserHelperClient userHelper,
                INotificationRepository notificationRepository,
                IClientConverterHelper clientConverterHelper)
        {
            _userHelper = userHelper;
            _notificationRepository = notificationRepository;
            _converterHelper = clientConverterHelper;
        }


        public IActionResult AllNotifications()
        {
            return View();
        }

        public JsonResult GetNotifications(string id, bool isGetOnlyUnread = false)
        {

            var modelList = new List<NotificationViewModel>();

            if (isGetOnlyUnread)
            {
                modelList = _notificationRepository.GetAllNotifications(id)
                    .Select(a => _converterHelper.ToNotificationViewModel(a))
                     .ToList();
            }
            else
            {
                modelList = _notificationRepository.GetUnreadNotifications(id)
                     .Select(a => _converterHelper.ToNotificationViewModel(a))
                     .ToList();
            }

            return Json(modelList);
        }


















    }
}
