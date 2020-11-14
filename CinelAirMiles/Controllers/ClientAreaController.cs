using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinelAirMiles.Helpers;
using CinelAirMiles.Models;
using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Enums;
using CinelAirMilesLibrary.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinelAirMiles.Controllers
{
    public class ClientAreaController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IClientConverterHelper _clientConverterHelper;
        private readonly ITransactionHelper _transactionHelper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IComplaintRepository _complaintRepository;

        public ClientAreaController(IUserHelper userHelper,
            ICountryRepository countryRepository,
            IReservationRepository reservationRepository,
            IClientConverterHelper clientConverterHelper,
            ITransactionHelper transactionHelper,
            ITransactionRepository transactionRepository,
            IComplaintRepository complaintRepository)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _reservationRepository = reservationRepository;
            _clientConverterHelper = clientConverterHelper;
            _transactionHelper = transactionHelper;
            _transactionRepository = transactionRepository;
            _complaintRepository = complaintRepository;
        }


        public IActionResult AccountManager()
        {
            return View();
        }


        #region USER UPDATE & PASSWORD UPDATE
        public async Task<IActionResult> UpdateClientInfo()
        {
            var user = await _userHelper.GetUserByUsernameAsync(this.User.Identity.Name);

            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.Name = user.Name;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.City = user.City;
                model.TIN = user.TIN;
                model.Name = user.Name;
                model.PhoneNumber = user.PhoneNumber;
            }

            model.Countries = _countryRepository.GetComboCountries();
            return PartialView(nameof(UpdateClientInfo),model);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateClientInfo(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByUsernameAsync(this.User.Identity.Name);
                    if (user != null)
                    {
                        var country = await _countryRepository.GetByIdAsync(model.CountryId);

                        user.Name = model.Name;
                        user.Address = model.Address;
                        user.PhoneNumber = model.PhoneNumber;
                        user.City = model.City;
                        user.Name = model.Name;
                        user.TIN = model.TIN;
                        user.Name = model.Name;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Country = country;

                        var respose = await _userHelper.UpdateUserAsync(user);
                        if (respose.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "User updated successfully!");
                            return View(model);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            else
            {
                return new NotFoundResult();
            }

            return View(model);
        }


        public IActionResult UpdatePassword()
        {
            return PartialView(nameof(UpdatePassword));
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByUsernameAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUserClient");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return PartialView(nameof(AccountManager),model);
        }

        #endregion


        #region DIGITAL CARD
        [HttpGet]
        public async Task<IActionResult> DigitalCard()
        {
            try
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction(nameof(AccountController.LoginClient), "Account");
                }

                var model = new DigitalCardViewModel
                {
                    Name = user.Name,
                    ClientNumber = user.GuidId,
                    TierType = user.Tier,
                    ExpirationDate = DateTime.Now.AddYears(1)
                };

                return PartialView("_DigitalCard", model);
            }
            catch (Exception)
            {
                //return new NotFoundViewResult("_Error500Client");
                return RedirectToAction(nameof(AccountManager));
            }

        }
        #endregion


        #region RESERVATIONS INDEX
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


            return PartialView(nameof(ReservationIndex),list);
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

        #endregion


        #region TRANSACTIONS
        [HttpGet]
        public async Task<IActionResult> TransactionIndex()
        {
            try
            {
                var user = await GetCurrentUser();
                if (user == null)
                {
                    return RedirectToAction(nameof(AccountController.LoginClient), "Account");
                }

                var list = await _transactionRepository.GetByClientIdAsync(user.Id);

                var modelList = list.Select(c => _clientConverterHelper.ToTransactionViewModel(c, user));

                return PartialView(modelList);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return PartialView();
            }
        }


        public IActionResult MilesIndex()
        {
            //todo or to delete ?
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Purchase()
        {
            try
            {
                var user = await GetCurrentUser();
                if (user == null)
                {
                    return RedirectToAction(nameof(AccountController.LoginClient), "Account");
                }

                var model = new TransactionViewModel
                {
                    Values = GetBlocks(),
                    StartBalance = user.BonusMiles
                };

                return PartialView("_Purchase", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView("_Purchase");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Purchase(TransactionViewModel model)
        {
            try
            {
                var user = await GetCurrentUser();
                if (user == null)
                {
                    return RedirectToAction(nameof(AccountController.LoginClient), "Account");
                }

                var operation = _transactionRepository.GetTransactionHistory(user);
                if (!operation)
                {
                    throw new Exception("You cannot complete this operation. You maximum value per year has been reached!");
                }

                var transaction = _clientConverterHelper.CreatePurchaseTransaction(model, user);
                transaction.StartBalance = user.BonusMiles;
                transaction.EndBalance = transaction.StartBalance + transaction.Value;

                user.BonusMiles += transaction.Value;
                var result2 = await _userHelper.UpdateUserAsync(user);

                if (!result2.Succeeded)
                {
                    //send error 
                }

                var result = await _transactionRepository.AddTransanctionAsync(transaction);
                if (!result.Success)
                {
                    ModelState.AddModelError(string.Empty,
                        "An error ocurred while submitting your request. Please try again.");

                    return PartialView("_Purchase");
                }

                ModelState.AddModelError(string.Empty,
                        "You purchase was successfull. Your balance should reflect it in the next hours.");

                return PartialView("_Purchase");
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public IActionResult ExtendMiles()
        {
            //TODO blocos de 2000 milhas e max de 20 000 milhas por ano (fazer validação)

            return PartialView("_ExtendMiles");
        }


        [HttpPost]
        public IActionResult ExtendMiles(TransactionViewModel model)
        {
            
            //validas durante 3 anos

            // é apenas possível para as milhas que estão a caducar
            //na sua próxima data de caducidade de milhas ????? = as proximas na fila, a caducar?


            return PartialView("_ExtendMiles");
        }


        [HttpGet]
        public async Task<IActionResult> TransferMiles()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.LoginClient), "Account");
            }

            var model = new TransactionViewModel
            {
                Values = GetBlocks()
            };

            return PartialView("_TransferMiles", model);
        }

        [HttpPost]
        public async Task<IActionResult> TransferMiles(TransactionViewModel model)
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.LoginClient), "Account");
            }

            var userTo = _userHelper.GetUserByGuidId(model.TransferTo.GuidId);
            if (userTo == null)
            {
                //send error user not found
            }

            var operation = _transactionRepository.GetTransactionHistory(user);
            if (!operation)
            {
                throw new Exception("You cannot complete this operation. You maximum value per year has been reached!");
            }

            var transaction = _clientConverterHelper.CreateTransferTransaction(model, user, userTo);
            transaction.EndBalance = transaction.StartBalance - transaction.Value;

            user.BonusMiles = transaction.EndBalance;

            var result2 = await _userHelper.UpdateUserAsync(user);

            userTo.BonusMiles += transaction.Value;

            var result3 = await _userHelper.UpdateUserAsync(userTo);

            if (!result2.Succeeded && !result3.Succeeded)
            {
                ModelState.AddModelError(string.Empty,
                   "An error ocurred while submitting your request. Please try again.");
                return PartialView("_TransferMiles");
            }
            
            var response = await _transactionRepository.AddTransanctionAsync(transaction);
            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty,
                    "An error ocurred while submitting your request. Please try again.");

                return PartialView("_TransferMiles");
            }
            ModelState.AddModelError(string.Empty,
                    "You transfer was successfull. Your balance should reflect it in the next hours.");

            return PartialView("_TransferMiles");
        }



        [HttpGet]
        public async Task<IActionResult> ConvertMiles()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.LoginClient), "Account");
            }

            var list = GetBlocks();
            var model = new TransactionViewModel
            {
                Values = list,
                StartBalance = user.BonusMiles
            };

            return PartialView("_ConvertMiles", model);
        }


        [HttpPost]
        public async Task<IActionResult> ConvertMiles(TransactionViewModel model)
        {
            try
            {
                var user = await GetCurrentUser();
                if (user == null)
                {
                    return RedirectToAction(nameof(AccountController.LoginClient), "Account");
                }

                if (user.BonusMiles < model.Value)
                {
                    throw new Exception("You do not have enough miles to complete this operation");
                }

                var operation = _transactionRepository.GetTransactionHistory(user);
                if (!operation)
                {
                    throw new Exception("You cannot complete this operation. You maximum value per year has been reached!");
                }

                var trans = _clientConverterHelper.CreateConversionTransaction(model, user);
                trans.StartBalance = user.BonusMiles;
                trans.EndBalance = user.BonusMiles;

                user.StatusMiles += model.Value;
                var result = await _userHelper.UpdateUserAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot process this operation at the moment. Please try again later.");
                }

                var response = await _transactionRepository.AddTransanctionAsync(trans);
                if (!response.Success)
                {
                    //TODO enviar notificação para superuser ou admin para introduzir a transacção à mão
                    throw new Exception("Culpa do utilizador");
                }

                return PartialView("_ConvertMiles", model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404Client");
            }
        }



        [HttpGet]
        public async Task<IActionResult> NominateToGold()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.LoginClient), "Account");
            }
            var model = new NominateViewModel();
            if (user.Tier == TierType.Silver || user.Tier == TierType.Gold)
            {
                //TODO novo bool para o cliente HasNominated
                model.CheckTier = true;
            }
            
            return PartialView("_NominateToGold", model);
        }
        #endregion


        #region USER REQUEST/COMPLAINTS
        [HttpGet]
        public async Task<ActionResult> ComplaintsIndex()
        {
            var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

            if (user == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var list = await _complaintRepository.GetClientComplaintsAsync(user.Id);

            var modelList = new List<ComplaintViewModel>(
                list.Select(c => _clientConverterHelper.ToComplaintClientViewModel(c))
                .ToList());

            return PartialView(nameof(ComplaintsIndex), modelList);
            //else
            //{
            //    string retUrl = Request.PathBase;
            //    return RedirectToAction("LoginClient", "Account", new { returnUrl = retUrl });
            //}
        }


        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

            if (user == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var complaint = await _complaintRepository.GetByIdAsync(id.Value);

            if (complaint == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var model = _clientConverterHelper.ToComplaintClientViewModel(complaint);

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var client = User.Identity.Name;

            if (client == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var model = new ComplaintViewModel
            {
                Complaints = _complaintRepository.GetComboComplaintTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComplaintViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                    if (user == null)
                    {
                        return new NotFoundViewResult("_Error404Client");
                    }

                    var clientComplaint = _clientConverterHelper.ToClientComplaint(model, true, user);

                    await _complaintRepository.CreateAsync(clientComplaint);

                    return RedirectToAction(nameof(ClientAreaController.ComplaintsIndex), nameof(ClientAreaController));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }
        #endregion


        private protected async Task<User> GetCurrentUser()
        {
            var user = User.Identity.Name;
            if (user == null)
            {
                return null;
            }
            return await _userHelper.GetUserByUsernameAsync(user);
        }


        private IEnumerable<SelectListItem> GetBlocks()
        {
            List<int> blocks = new List<int>();
            for (int i = 2000; i <= 20000; i += 2000)
            {
                blocks.Add(i);
            }

            var modelList = blocks.Select(b => new SelectListItem
            {
                Value = b.ToString(),
                Text = b.ToString()
            });

            return modelList;
        }

    }
}
