namespace CinelAirMiles.Controllers
{
    using global::CinelAirMiles.Data.Repositories;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Helpers;
    using System;
    using System.Threading.Tasks;

    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserHelper _userHelper;

        public ReservationController(IReservationRepository reservationRepository,
            IUserHelper userHelper)
        {
            _reservationRepository = reservationRepository;
            _userHelper = userHelper;
        }



        [HttpGet]
        public async Task<IActionResult> ReservationIndex()
        {
            var currentUser = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var clientReservation = _reservationRepository.GetCurrentClientByIdAsync(currentUser.Id);

            if (clientReservation == null)
            {
                return NotFound();
            }

            var model = new ReservationViewModel
            {
                ReservationId = clientReservation.Id,
                ClientName = currentUser.Name
            };

            return View(model);
        }

        public async Task<IActionResult> CancelReservation(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

            try
            {
                var clientReservation = await _reservationRepository.GetByIdAsync(id.Value);

                if (clientReservation == null)
                {
                    return new NotFoundViewResult("_UserNotFound");

                }

                clientReservation.ModifiedBy = await _userHelper.GetUserByIdAsync(clientReservation.Id.ToString());
                clientReservation.UpdateDate = DateTime.Now;
                clientReservation.Status = 6;

               await _reservationRepository.UpdateAsync(clientReservation);


                return RedirectToAction(nameof(ReservationIndex));

            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound");//todo mudar 
            }

        }


    }
}