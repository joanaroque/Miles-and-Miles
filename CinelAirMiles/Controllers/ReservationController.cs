namespace CinelAirMiles.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CinelAirMiles.Helpers;

    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Helpers;

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


                return RedirectToAction(nameof(ClientAreaController.ReservationIndex), nameof(ClientAreaController));

            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return RedirectToAction(nameof(ClientAreaController.ReservationIndex), nameof(ClientAreaController));
        }
    }
}