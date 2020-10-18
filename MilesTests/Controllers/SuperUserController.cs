namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Data;
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Data.Repositories;
    using MilesBackOffice.Web.Data.Repositories.SuperUser;
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

        /// <summary>
        /// get list of unconfirm tiers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> TierChange()
        {
            List<TierChangeViewModel> modelList = await _clientRepository.GetPendingTierClient();

            return View(modelList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmTierChange(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new NotFoundViewResult("UserNotFound");
            }

            try
            {
                var user = await _userHelper.GetUserByIdAsync(id);
                if (user == null)
                {
                    return new NotFoundViewResult("UserNotFound");
                }

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

        /// <summary>
        /// get list of unprocessed complaints
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Complaints()
        {
            List<ComplaintClientViewModel> modelList = await _clientRepository.GetClientComplaints();

    //            if (user == null)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }

        /// <summary>
        /// receive complaint Id and return "Complaint details" view model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET 
        [HttpGet]
        public async Task<IActionResult> ComplaintReply(string id)
        {
            var list = await _clientRepository.GetClientComplaints();

            ComplaintClientViewModel selectedViewModel = list
                                                   .Where(complaint => complaint.ComplaintId.Equals(id))
                                                   .FirstOrDefault();

            return View(selectedViewModel);
        }

        /// <summary>
        /// validate if reply is filled. If not, send error. Otherwise, continue.
        /// update repository with new reply for the incoming complaint Id and change IsProcessed to 'true'.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ComplaintReply(ComplaintClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByIdAsync(model.UserId);

                    if (user == null)
                    {
                        return new NotFoundViewResult("UserNotFound");
                    }

                    model.IsProcessed = true;

                    var result = await _userHelper.UpdateUserAsync(user);

    //            }
    //            catch (Exception)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }
    //        }
    //        return View(model);
    //    }

                }
                catch (Exception)
                {
                    return new NotFoundViewResult("UserNotFound");
                }
            }
            return View(model);

        }

        /// <summary>
        /// get list of seats to be confirmed
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AvailableSeats()
        {
            List<AvailableSeatsViewModel> modelList = await _clientRepository.GetSeatsToBeConfirm();

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="Id"></param>
    //    /// <returns></returns>
    //    public async Task<IActionResult> ConfirmAvailableSeats(string Id)
    //    {
    //        if (string.IsNullOrEmpty(Id))
    //        {
    //            return new NotFoundViewResult("UserNotFound");
    //        }

    //        try
    //        {
    //            var user = await _userHelper.GetUserByIdAsync(Id);

    //            if (user == null)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }

    //            //    user.PendingSeatsAvailable = true;

                user.PendingSeatsAvailable = true;


    //            return RedirectToAction(nameof(AvailableSeats));
    //        }
    //        catch (Exception)
    //        {
    //            return new NotFoundViewResult("UserNotFound");

    //        }
    //    }

    //    /// <summary>
    //    /// get list of advertising to be confirmed
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public async Task<ActionResult> AdvertisingAndReferences()
    //    {
    //        var list = await _advertisingRepository.GetAdvertisingToBeConfirmAsync();

        /// <summary>
        /// get list of advertising to be confirmed
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AdvertisingAndReferences()
        {
            List<AdvertisingViewModel> modelList = await _clientRepository.GetAdvertisingToBeConfirm();

    //        return View(modelList);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="Id"></param>
    //    /// <returns></returns>
    //    public async Task<IActionResult> ConfirmAdvertisingAndReferences(string Id)
    //    {
    //        if (string.IsNullOrEmpty(Id))
    //        {
    //            return new NotFoundViewResult("UserNotFound");
    //        }

    //        try
    //        {
    //            var user = await _userHelper.GetUserByIdAsync(Id);

    //            if (user == null)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }

                user.PendingAdvertising = true;

    //            var result = await _userHelper.UpdateUserAsync(user);


    //            return RedirectToAction(nameof(AvailableSeats));
    //        }
    //        catch (Exception)
    //        {
    //            return new NotFoundViewResult("UserNotFound");

    //        }
    //    }
    }
}