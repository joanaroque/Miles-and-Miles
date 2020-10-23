namespace MilesBackOffice.Web.Controllers
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Data.Repositories.SuperUser;
    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.SuperUser;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SuperUserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ITierChangeRepository _tierChangeRepository;
        private readonly IClientComplaintRepository _clientComplaintRepository;

        public SuperUserController(IUserHelper userHelper,
            IAdvertisingRepository advertisingRepository,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            ITierChangeRepository tierChangeRepository,
            IClientComplaintRepository clientComplaintRepository)
        {
            _userHelper = userHelper;
            _advertisingRepository = advertisingRepository;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _tierChangeRepository = tierChangeRepository;
            _clientComplaintRepository = clientComplaintRepository;
        }

        /// <summary>
        /// get list of all clients and transforms an entity to viewmodel
        /// </summary>
        /// <returns>the list of all clients</returns>
        [HttpGet]
        public async Task<ActionResult> TierChange()
        {
            var list = await _tierChangeRepository.GetAllClientListAsync(); // mysteriously, the enum started to work

            var modelList = new List<TierChangeViewModel>(
                list.Select(a => _converterHelper.ToTierChangeViewModel(a))
                .ToList());

            return View(modelList);
        }

        /// <summary>
        /// confirm tier change and updated the data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>teh TierChange view</returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmTierChange(int? id) //tierChange Id
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

            try
            {
                TierChange tierChange = await _tierChangeRepository.GetByIdWithIncludesAsync(id.Value);

                if (tierChange == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                var user = await _userHelper.GetUserByIdAsync(tierChange.Client.Id);

                if (user == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                tierChange.ModifiedBy = user;
                tierChange.UpdateDate = DateTime.Now;
                tierChange.Status = 0;

                await _tierChangeRepository.UpdateAsync(tierChange);

                _mailHelper.SendMail(user.Email, $"Your Tier change has been confirmed.",
               $"<h1>You can now use our service as a {tierChange.NewTier}.</h1>");

                //  todo:  ViewBag.Message = "An error ocurred. Try again please.";

                return RedirectToAction(nameof(TierChange));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound"); //todo: mudar erros
            }
        }

        /// <summary>
        /// get list of all complaints and transforms an entity to viewmodel
        /// </summary>
        /// <returns>a list of complaints</returns>
        [HttpGet]
        public async Task<ActionResult> Complaints()
        {
            var list = await _clientComplaintRepository.GetClientComplaintsAsync();

            var modelList = new List<ComplaintClientViewModel>(
                list.Select(c => _converterHelper.ToComplaintClientViewModel(c))
                .ToList());

            return View(modelList);
        }

        /// <summary>
        /// receive complaint Id and return "Complaint details" view model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET 
        [HttpGet]
        public async Task<IActionResult> ComplaintReply(int? id)
        {
            var entityList = await _clientComplaintRepository.GetClientComplaintsAsync();

            ClientComplaint selectedComplaint = entityList
                                                   .Where(complaint => complaint.Id.Equals(id.Value))
                                                   .FirstOrDefault();


            return View(_converterHelper.ToComplaintClientViewModel(selectedComplaint));
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
                    var complaint = await _clientComplaintRepository.GetByIdWithIncludesAsync(model.ComplaintId);

                    if (complaint == null)
                    {
                        return new NotFoundViewResult("_UserNotFound");

                    }

                    var user = await _userHelper.GetUserByIdAsync(complaint.CreatedBy.Id);

                    if (user == null)
                    {
                        return new NotFoundViewResult("_UserNotFound");
                    }

                    complaint.ModifiedBy = user;
                    complaint.UpdateDate = DateTime.Now;
                    complaint.Status = 0;

                    await _clientComplaintRepository.UpdateAsync(complaint);

                    _mailHelper.SendMail(user.Email, $"Your complaint has been processed.",
                       $"<h1>You are very important for us.\nThank you very much.</h1>");

                    //todo:  ViewBag.Message = "An error ocurred. Try again please.";

                    return RedirectToAction(nameof(Complaints));

                }
                catch (Exception)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }
            }
            return View(model);
        }


        /// <summary>
        /// get list of all advertising and transforms an entity to viewmodel
        /// </summary>
        /// <returns>a list of advertising</returns>
        [HttpGet]
        public async Task<ActionResult> AdvertisingAndReferences()
        {
            var list = await _advertisingRepository.GetAllAdvertisingAsync();

            var modelList = new List<AdvertisingViewModel>(
                list.Select(a => _converterHelper.ToAdvertisingViewModel(a))
                .ToList());

            return View(modelList);
        }

        /// <summary>
        /// details from advertising content
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>the selected advertising</returns>
        public async Task<IActionResult> AdvertisingDetails(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }


            var entityList = await _advertisingRepository.GetAllAdvertisingAsync();

            Advertising selectedAdvertising = entityList
                                                   .Where(a => a.Id.Equals(id.Value))
                                                   .FirstOrDefault();


            return View(_converterHelper.ToAdvertisingViewModel(selectedAdvertising));

        }

        /// <summary>
        /// confirm the advertising and updated the data
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns>AdvertisingAndReferences view</returns>
        public async Task<IActionResult> ConfirmAdvertisingAndReferences(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

            try
            {
                Advertising advertising = await _advertisingRepository.GetByIdWithIncludesAsync(id.Value);

                if (advertising == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                advertising.ModifiedBy = await _userHelper.GetUserByIdAsync(advertising.Id.ToString());
                advertising.UpdateDate = DateTime.Now;
                advertising.Status = 0;

                await _advertisingRepository.UpdateAsync(advertising);

                return RedirectToAction(nameof(AdvertisingAndReferences));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound");

            }
        }

        /// <summary>
        /// cancel the publish advertising and updated the data
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>the AdvertisingAndReferences view</returns>
        public async Task<IActionResult> CancelPublishAdvertising(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

            try
            {
                var advertising = await _advertisingRepository.GetByIdAsync(id.Value);

                if (advertising == null)
                {
                    return new NotFoundViewResult("_UserNotFound");

                }

                advertising.ModifiedBy = await _userHelper.GetUserByIdAsync(advertising.Id.ToString());
                advertising.UpdateDate = DateTime.Now;
                advertising.Status = 2;

                await _advertisingRepository.UpdateAsync(advertising);


                return RedirectToAction(nameof(AdvertisingAndReferences));

            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

        }

        /// <summary>
        /// cancel the tier change and updated the data
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>the TierChange view</returns>
        public async Task<IActionResult> CancelTierChange(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

            try
            {
                var tierChange = await _tierChangeRepository.GetByIdAsync(id.Value);

                if (tierChange == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                tierChange.ModifiedBy = await _userHelper.GetUserByIdAsync(tierChange.Id.ToString());
                tierChange.UpdateDate = DateTime.Now;
                tierChange.Status = 2;

                await _tierChangeRepository.UpdateAsync(tierChange);

                return RedirectToAction(nameof(TierChange));

            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

        }
    }
}