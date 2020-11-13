namespace CinelAirMiles.Controllers
{
    using CinelAirMiles.Helpers;
    using CinelAirMiles.Models;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using CinelAirMilesLibrary.Common.Web.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using MilesBackOffice.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserHelper _userHelper;
        private readonly IClientConverterHelper _clientConverterHelper;

        public ReservationController(IReservationRepository reservationRepository,
            IUserHelper userHelper,
            IClientConverterHelper clientConverterHelper)
        {
            _reservationRepository = reservationRepository;
            _userHelper = userHelper;
            _clientConverterHelper = clientConverterHelper;
        }


        [HttpGet]
        public async Task<IActionResult> ReservationIndex()
        {
            var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var clientReservation = await _reservationRepository.GetReservationsFromCurrentClientToListAsync(user.Id);

            var list = new List<ReservationViewModel>(clientReservation.
                Select(c => _clientConverterHelper.ToReservationViewModel(c)).
                ToList());


            return View(list);
        }

        public async Task<IActionResult> CancelReservation(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            try
            {
                var clientReservation = await _reservationRepository.GetByIdAsync(id.Value);

                if (clientReservation == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }

                clientReservation.ModifiedBy = await _userHelper.GetUserByIdAsync(clientReservation.Id.ToString());
                clientReservation.UpdateDate = DateTime.Now;
                clientReservation.Status = 6;

                await _reservationRepository.UpdateAsync(clientReservation);


                return RedirectToAction(nameof(ReservationIndex));

            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return RedirectToAction(nameof(ReservationIndex));
        }
    }
}