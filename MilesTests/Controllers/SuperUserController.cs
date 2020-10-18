namespace MilesBackOffice.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Data.Repositories.SuperUser;
    using MilesBackOffice.Web.Helpers;
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
        private readonly ISeatsAvailableRepository _seatsAvailableRepository;

        public SuperUserController(IUserHelper userHelper,

            IAdvertisingRepository advertisingRepository,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            ITierChangeRepository tierChangeRepository,
            IClientComplaintRepository clientComplaintRepository,
            ISeatsAvailableRepository seatsAvailableRepository)
        {
            _userHelper = userHelper;
            _advertisingRepository = advertisingRepository;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _tierChangeRepository = tierChangeRepository;
            _clientComplaintRepository = clientComplaintRepository;
            _seatsAvailableRepository = seatsAvailableRepository;
        }

        /// <summary>
        /// get list of unconfirm tiers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> TierChange()
        {
            var list = await _tierChangeRepository.GetPendingTierClientListAsync();

            var modelList = new List<TierChangeViewModel>(
                list.Select(a => _converterHelper.ToTierChangeViewModel(a))
                .ToList());

            return View(modelList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

                tierChange.IsConfirm = true;
                tierChange.CreatedBy = user;
                tierChange.CreateDate = DateTime.Now;

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
        /// get list of unprocessed complaints
        /// </summary>
        /// <returns></returns>
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
                    var complaint = await _clientComplaintRepository.GetByIdAsync(model.ComplaintId);

                    if (complaint == null)
                    {
                        return new NotFoundViewResult("_UserNotFound");

                    }

                    var user = await _userHelper.GetUserByIdAsync(complaint.Client.Id);

                    if (user == null)
                    {
                        return new NotFoundViewResult("_UserNotFound");
                    }

                    complaint.PendingComplaint = true;
                    complaint.CreatedBy = user;
                    complaint.CreateDate = DateTime.Now;

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
        /// get list of seats to be confirmed
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AvailableSeats()
        {
            var list = await _seatsAvailableRepository.GetSeatsToBeConfirmAsync();

            var modelList = new List<AvailableSeatsViewModel>(
                list.Select(a => _converterHelper.ToAvailableSeatsViewModel(a))
                .ToList());

            return View(modelList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ConfirmAvailableSeats(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_UserNotFound");
            }

            try
            {
                SeatsAvailable seatsAvailable = await _seatsAvailableRepository.GetByIdWithIncludesAsync(id.Value);

                if (seatsAvailable == null)
                {
                    return new NotFoundViewResult("_UserNotFound"); //todo mudar erros
                }

                seatsAvailable.ConfirmSeatsAvailable = true;

                await _seatsAvailableRepository.UpdateAsync(seatsAvailable);


                return RedirectToAction(nameof(AvailableSeats));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound");
            }
        }

        /// <summary>
        /// get list of advertising to be confirmed
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AdvertisingAndReferences()
        {
            var list = await _advertisingRepository.GetAdvertisingToBeConfirmAsync();

            var modelList = new List<AdvertisingViewModel>(
                list.Select(a => _converterHelper.ToAdvertisingViewModel(a))
                .ToList());

            return View(modelList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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

                advertising.PendingPublish = true;
               
                await _advertisingRepository.UpdateAsync(advertising);

                advertising.CreatedBy = await _userHelper.GetUserByIdAsync(advertising.Id.ToString()); //******************************************************************
                advertising.CreateDate = DateTime.Now; //todo: fazer cancel ao pe do confirm

                return RedirectToAction(nameof(AvailableSeats));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_UserNotFound");

            }
        }
    }
}