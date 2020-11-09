namespace CinelAirMiles.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc;
    using MilesBackOffice.Web.Helpers;

    //This is the shop
    public class PremiumOffersController : Controller
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly ITransactionHelper _transactionHelper;
        private readonly IUserHelper _userHelper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IReservationRepository _reservationRepository;

        public PremiumOffersController(IPremiumRepository premiumRepository,
            ITransactionHelper transactionHelper,
            IUserHelper userHelper,
            ITransactionRepository transactionRepository,
            IReservationRepository reservationRepository)
        {
            _premiumRepository = premiumRepository;
            _transactionHelper = transactionHelper;
            _userHelper = userHelper;
            _transactionRepository = transactionRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _premiumRepository.GetAllOffersAsync());
        }

        /// <summary>
        /// Completes a purchase from a User(client) 
        /// Updates the User's BonusMiles
        /// Creates a Transaction with details of the purchase
        /// If it's a ticket, creates a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> PurchasePremium(int id)
        {
            if (id == 0)
            {
                return new NotFoundViewResult("_Error404Client");
            }
            try
            {
                var offer = await _premiumRepository.GetByIdAsync(id);
                if (offer == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }

                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
                if (user == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }
                //create a transaction
                var trans = _transactionHelper.CreatePurchaseTransaction(user, offer);

                var response = await _transactionRepository.AddTransanctionAsync(trans);
                if (!response.Success)
                {
                    //send notification to admin to check transaction
                    //send an error to User saying that a problem ocurred with the purchase
                    throw new Exception(response.Message);
                }

                //criar a reserva
                //guardar na BD
                //enviar a reserva pelo email

                user.BonusMiles = trans.EndBalance;
                var result = await _userHelper.UpdateUserAsync(user);

                //update User fails but transaction and reservation were successfull
                if (!result.Succeeded)
                {
                    throw new Exception("You purchase was successfull. Your balance should reflect it in the next hours.");
                }

                return View(); //return success message
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return View(); //return success message

        }

        public async Task<IActionResult> CancelReservation(int id) //Id from reservation not ReservationId
        {
            if (id == 0)
            {
                return new NotFoundViewResult("_Error404Client");
            }
            try
            {
                var reservation = await _reservationRepository.GetByIdIncludingAsync(id);
                if (reservation == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }
                reservation.Status = 3;
                var result = await _reservationRepository.UpdateReservationAsync(reservation);
                if (!result.Success)
                {
                    throw new Exception(result.Message);
                }
                var transaction = _transactionHelper.CreateCancelPurchaseTransaction(reservation);

                result = await _transactionRepository.AddTransanctionAsync(transaction);
                if (!result.Success)
                {
                    //cria uma Notification para o Admin
                    throw new Exception("An error ocurred while submitting your request. Please try again");
                }

                //all goes well
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
