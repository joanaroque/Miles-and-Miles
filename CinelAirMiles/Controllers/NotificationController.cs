namespace CinelAirMiles.Controllers
{
    using CinelAirMiles.Helpers;
    using CinelAirMiles.Models;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class NotificationController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly INotificationRepository _notificationRepository;
        private readonly IClientConverterHelper _converterHelper;

        public NotificationController(IUserHelper userHelper,
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

        [HttpGet]
        public async Task<ActionResult> NotificationsIndex()
        {
            var currentUser = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var noti = _notificationRepository.GetAllNotifications(currentUser.Id);

            return View(noti);
        }

        [HttpGet]
        public async Task<IActionResult> MarkAsRead(int? id)
        {
            if (id == null)
            {
                return NotFound(); // todo mudar erros
            }

            try
            {
                Notification notification = await _notificationRepository.GetUnreadNotifications(id.Value);

                if (notification == null)
                {
                    return NotFound();
                }

                var user = await _userHelper.GetUserByIdAsync(notification.CreatedBy.Id);

                if (user == null)
                {
                    return NotFound();
                }

                notification.ModifiedBy = user;
                notification.UpdateDate = DateTime.Now;
                notification.Status = 7; // noti read in bd

                await _notificationRepository.UpdateAsync(notification);


                return RedirectToAction(nameof(NotificationsIndex));
            }
            catch (Exception)
            {
                return NotFound();
            }
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
            //else
            //{
            //    modelList = _notificationRepository.GetUnreadNotifications(int.Parse(id))
            //         .Select(a => _converterHelper.ToNotificationViewModel(a))
            //         .ToList();
            //}

            return Json(modelList);
        }



    }
}
