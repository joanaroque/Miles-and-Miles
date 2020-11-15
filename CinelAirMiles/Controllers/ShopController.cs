namespace CinelAirMiles.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMiles.Helpers;

    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    //This is the shop
    public class ShopController : Controller
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly ITransactionHelper _transactionHelper;
        private readonly IUserHelper _userHelper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IClientConverterHelper _converterHelper;

        public ShopController(IPremiumRepository premiumRepository,
            ITransactionHelper transactionHelper,
            IUserHelper userHelper,
            ITransactionRepository transactionRepository,
            IReservationRepository reservationRepository,
            IClientConverterHelper converterHelper)
        {
            _premiumRepository = premiumRepository;
            _transactionHelper = transactionHelper;
            _userHelper = userHelper;
            _transactionRepository = transactionRepository;
            _reservationRepository = reservationRepository;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _premiumRepository.GetAllOffersAsync());
        }



        public async Task<IActionResult> PremiumDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("This item was not found!");
                }

                var model = await _premiumRepository.GetByIdWithIncludesAsync(id.Value);
                if (model == null)
                {
                    throw new Exception("This item was not found!");
                }

                return View(_converterHelper.ToPremiumOfferViewModel(model));
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Completes a purchase from a User(client) 
        /// Updates the User's BonusMiles
        /// Creates a Transaction with details of the purchase
        /// If it's a ticket, creates a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> PurchasePremium(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("No item found!");
                }
                
                var offer = await _premiumRepository.GetByIdAsync(id.Value);
                if (offer == null)
                {
                    throw new Exception("No item found!");
                }

                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
                if (user == null)
                {
                    await _userHelper.LogoutAsync();
                    return RedirectToAction(nameof(HomeController.IndexClient), nameof(HomeController));
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


        public IActionResult SearchOffers(string departure, string arrival, string type)
        {
            var list = _premiumRepository.SearchByParameters(departure, arrival, type);

            var modelList = list.Select(i => _converterHelper.ToPremiumOfferViewModel(i));

            return PartialView("_OfferList", modelList);
        }

        public async Task<IActionResult> CancelReservation(int id) //Id from reservation not ReservationId
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("No item found!");
                }

                var reservation = await _reservationRepository.GetByIdIncludingAsync(id);
                if (reservation == null)
                {
                    throw new Exception("No item found!");
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
