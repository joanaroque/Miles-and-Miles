namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Data;
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Data.Repositories;
    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.SuperUser;

    public class SuperUserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IClientRepository _clientRepository;
        private readonly IMailHelper _mailHelper;

        public SuperUserController(IUserHelper userHelper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IClientRepository clientRepository,
            IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _clientRepository = clientRepository;
            _mailHelper = mailHelper;
        }


        [HttpGet]
        public ActionResult TierChange()
        {
            //lista de clientes com tier pendente
            List<TierChangeViewModel> modelList = _clientRepository.GetPendingTierClient();

            return View(modelList);
        }


        public async Task<IActionResult> ConfirmTierChange(string clientUserId)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return new NotFoundViewResult("UserNotFound");
            }

            try
            {
                var user = await _userHelper.GetUserByIdAsync(clientUserId);
                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                //confirm tier change
                user.PendingTier = true;

                var result = await _userHelper.UpdateUserAsync(user);

                if (result.Succeeded)
                {
                    _mailHelper.SendMail(user.Email, $"Your Tier change has been confirmed.",
                   $"<h1>You can now use our service as a --------------.</h1>");
                }//todo: por nome do novo tier
                else
                {
                    ViewBag.Message = "An error ocurred. Try again please.";
                }
                return RedirectToAction(nameof(TierChange));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("UserNotFound");
            }
        }

        [HttpGet]
        public ActionResult Complaints()
        {
            //lista reclamaçoes nao processadas
            List<ComplaintClientViewModel> modelList = _clientRepository.GetClientComplaint();

            return View(modelList);
        }

        public async Task<IActionResult> ComplaintReply(ComplaintClientViewModel model)
        {
            var user = await _userHelper.GetUserByIdAsync(model.Id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }




            return View();
        }

        [HttpGet]
        public ActionResult AvailableSeats()
        {
            //lista de de lugares por confirmar
            List<AvailableSeatsViewModel> modelList = _clientRepository.GetSeatsToBeConfirm();

            return View(modelList);
        }

        public async Task<IActionResult> ConfirmAvailableSeats(string clientUserId)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return new NotFoundViewResult("UserNotFound");
            }

            try
            {
                var user = await _userHelper.GetUserByIdAsync(clientUserId);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                user.PendingSeatsAvailable = true;

                var result = await _userHelper.UpdateUserAsync(user);


                return RedirectToAction(nameof(AvailableSeats));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("UserNotFound");

            }
        }

        [HttpGet]
        public ActionResult AdvertisingAndReferences()
        {
            //lista de publicidade por confirmar
            List<AdvertisingViewModel> modelList = _clientRepository.GetAdvertisingToBeConfirm();

            return View(modelList);
        }


        public async Task<IActionResult> ConfirmAdvertisingAndReferences(string clientUserId)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return new NotFoundViewResult("UserNotFound");
            }

            try
            {
                var user = await _userHelper.GetUserByIdAsync(clientUserId);

                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

                user.PendingAdvertising = true;

                var result = await _userHelper.UpdateUserAsync(user);


                return RedirectToAction(nameof(AvailableSeats));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("UserNotFound");

            }
        }
    }
}