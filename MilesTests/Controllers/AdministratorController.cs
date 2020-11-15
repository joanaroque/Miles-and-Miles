namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.Admin;

    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IClientRepository _clientRepository;
        private readonly IComplaintRepository _complaintRepository;
        private readonly INotificationRepository _notificationRepository;

        public AdministratorController(
            IUserHelper userHelper,
            ICountryRepository countryRepository,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            IClientRepository clientRepository,
            IComplaintRepository complaintRepository,
            INotificationRepository notificationRepository)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _clientRepository = clientRepository;
            _complaintRepository = complaintRepository;
            _notificationRepository = notificationRepository;
        }


        public IActionResult NewClients()
        {
            var usersList = _clientRepository.GetNewClients();

            var list = usersList.Select(u => _converterHelper.ToNewClientViewModel(u));

            return View(list);
        }

        #region NEW CLIENTS
        public async Task<IActionResult> ApproveClient(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new DBConcurrencyException();
                }

                var user = await _userHelper.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new DBConcurrencyException();
                }

                user.IsApproved = true;
                user.AccountApprovedDate = DateTime.Now;

                var result = await _userHelper.UpdateUserAsync(user);

                if (!result.Succeeded)
                {
                    throw new DBConcurrencyException();
                }

                _mailHelper.SendApproveClient(user.Email, user.Name, user.GuidId);

                return RedirectToAction(nameof(ListUsers));
            }
            catch (DBConcurrencyException)
            {
                return new NotFoundViewResult("_Error404");
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }


        public async Task<IActionResult> DeclineClient(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new DBConcurrencyException();
                }

                var user = await _userHelper.GetUserByIdAsync(id);

                if (user == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                var result = await _userHelper.DeleteUserAsync(user);

                if (!result.Success)
                {
                    throw new DBConcurrencyException();
                }

                _mailHelper.SendRefuseClient(user.Email, user.Name);

                return RedirectToAction(nameof(ListUsers));
            }
            catch (DBConcurrencyException)
            {
                return new NotFoundViewResult("_Error404");
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }
        #endregion

        #region REGISTER NEW USER
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Roles = _userHelper.GetComboRoles(),
                Genders = _clientRepository.GetComboGenders()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.EmailAddress);

                if (user == null)
                {
                    try
                    {
                        var country = await _countryRepository.GetByIdAsync(model.CountryId);
                        user = _converterHelper.ToUser(model, country);

                        var password = UtilityHelper.Generate();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return View(model);
                        }

                        await _userHelper.AddUSerToRoleAsync(user, user.SelectedRole);

                        var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                        var link = Url.Action(
                            "ResetPassword",
                            "Account",
                            new
                            {
                                token = myToken,
                                userId = user.Id
                            },
                            protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendToNewUser(user.Email, link, user.Name);

                        return RedirectToAction(nameof(NewClients));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This username is already registered.");
                }
            }
            model.Countries = _countryRepository.GetComboCountries();
            model.Roles = _userHelper.GetComboRoles();
            model.Genders = _clientRepository.GetComboGenders();
            return View(model);
        }
        #endregion

        #region USER ACTIONS LIST/DETAILS/DELETE
        [HttpGet]
        public ActionResult ListUsers()
        {
            var users = _clientRepository.GetActiveUsers();
            
            var modelList = users.Select(u => _converterHelper.ToUserViewModel(u));

            return View(modelList);
        }

        /// <summary>
        /// Partial View
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Partial View with Client Personal Details</returns>
        public async Task<IActionResult> DetailsUser(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                if (string.IsNullOrEmpty(id))
                {
                    throw new DBConcurrencyException();
                }

                var user = await _userHelper.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new DBConcurrencyException();
                }

                var model = _converterHelper.ToUserViewModel(user);

                return PartialView("_DetailsUser", model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404");
            }
        }


        [HttpGet]
        public async Task<IActionResult> DetailsUserPage(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new DBConcurrencyException();
                }

                var user = await _userHelper.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new DBConcurrencyException();
                }

                var model = _converterHelper.ToUserViewModel(user);

                return View(model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404");
            }
        }



        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userHelper.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new Exception();
                }

                var result = await _userHelper.DeleteUserAsync(user);

                if (!result.Success)
                {
                    throw new Exception();
                }

                return RedirectToAction("ListUsers");
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_404Error");
            }
        }


        public async Task<IActionResult> SendResetPassEmail(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    //error    
                }

                var user = await _userHelper.GetUserByGuidIdAsync(id);
                if (user == null)
                {
                    //error
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new
                    {
                        validToken = myToken,
                        userId = user.GuidId
                    }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendResetEmail(user.Email, user.Name, link);

                var notification = await _notificationRepository.GetByGuidIdAndTypeAsync(id, NotificationType.Recover);
                await _notificationRepository.DeleteAsync(notification);

                return RedirectToAction(nameof(UserNotifications));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region NOTIFICATIONS
        public async Task<IActionResult> UserNotifications()
        {
            var notifications = await _notificationRepository.GetNotificationsByRoleAsync(UserType.Admin);

            var userList = await _userHelper.GetUsersInListAsync(notifications);

            var modelList = userList.Select(u => _converterHelper.ToNotifyViewModel(u));

            
            return View("_NewClients", modelList);
        }
        #endregion

        public IActionResult UserNotFound()
        {
            return new NotFoundViewResult("_Error404");
        }
    }
}