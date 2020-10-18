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
    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.SuperUser;

    public class SuperUserController : Controller
    {
    //    private readonly IUserHelper _userHelper;
    //    private readonly RoleManager<IdentityRole> _roleManager;
    //    private readonly UserManager<User> _userManager;
    //    private readonly IAdvertisingRepository _advertisingRepository;
    //    private readonly IMailHelper _mailHelper;
    //    private readonly IConverterHelper _converterHelper;
    //    private readonly ITierChangeRepository _tierChangeRepository;
    //    private readonly IClientComplaintRepository _clientComplaintRepository;
    //    private readonly ISeatsAvailableRepository _seatsAvailableRepository;

    //    public SuperUserController(IUserHelper userHelper,
    //        RoleManager<IdentityRole> roleManager,
    //        UserManager<User> userManager,
    //        IAdvertisingRepository advertisingRepository,
    //        IMailHelper mailHelper,
    //        IConverterHelper converterHelper,
    //        ITierChangeRepository tierChangeRepository,
    //        IClientComplaintRepository clientComplaintRepository,
    //        ISeatsAvailableRepository seatsAvailableRepository)
    //    {
    //        _userHelper = userHelper;
    //        _roleManager = roleManager;
    //        _userManager = userManager;
    //        _advertisingRepository = advertisingRepository;
    //        _mailHelper = mailHelper;
    //        _converterHelper = converterHelper;
    //        _tierChangeRepository = tierChangeRepository;
    //        _clientComplaintRepository = clientComplaintRepository;
    //        _seatsAvailableRepository = seatsAvailableRepository;
    //    }

    //    /// <summary>
    //    /// get list of unconfirm tiers
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public async Task<ActionResult> TierChange()
    //    {
    //        var list = await _tierChangeRepository.GetPendingTierClientListAsync();

    //        var modelList = new List<TierChangeViewModel>(
    //            list.Select(a => _converterHelper.ToTierChangeViewModel(a))
    //            .ToList());

    //        return View(modelList);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public async Task<IActionResult> ConfirmTierChange(int? id) //tierChange Id
    //    {
    //        try
    //        {
    //            TierChange tierChange = await _tierChangeRepository.GetByIdWithIncludesAsync(id.Value);

    //            if (tierChange == null)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }

    //            tierChange.IsConfirm = true;

    //            await _tierChangeRepository.UpdateAsync(tierChange);

    //            var user = await _userHelper.GetUserByIdAsync(tierChange.Client.Id);

    //            if (user == null)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }


    //            _mailHelper.SendMail(user.Email, $"Your Tier change has been confirmed.",
    //           $"<h1>You can now use our service as a {tierChange.NewTier}.</h1>");

    //            //    ViewBag.Message = "An error ocurred. Try again please.";

    //            return RedirectToAction(nameof(TierChange));
    //        }
    //        catch (Exception)
    //        {
    //            return new NotFoundViewResult("UserNotFound");
    //        }
    //    }

    //    /// <summary>
    //    /// get list of unprocessed complaints
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public async Task<ActionResult> Complaints()
    //    {
    //        var list = await _clientComplaintRepository.GetClientComplaintsAsync();

    //        var modelList = new List<ComplaintClientViewModel>(
    //            list.Select(a => _converterHelper.ToComplaintClientViewModel(a))
    //            .ToList());

    //        return View(modelList);
    //    }

    //    /// <summary>
    //    /// receive complaint Id and return "Complaint details" view model
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    // GET 
    //    [HttpGet]
    //    public async Task<IActionResult> ComplaintReply(string id)
    //    {
    //        var entityList = await _clientComplaintRepository.GetClientComplaintsAsync();

    //        ClientComplaint selectedComplaint = entityList
    //                                               .Where(complaint => complaint.Id.Equals(id))
    //                                               .FirstOrDefault();

    //        var view = _converterHelper.ToComplaintClientViewModel(selectedComplaint);

    //        return View(view);
    //    }

    //    /// <summary>
    //    /// validate if reply is filled. If not, send error. Otherwise, continue.
    //    /// update repository with new reply for the incoming complaint Id and change IsProcessed to 'true'.
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    [HttpPost]
    //    public async Task<IActionResult> ComplaintReply(ComplaintClientViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var user = await _userHelper.GetUserByIdAsync(model.UserId);

    //            if (user == null)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }

    //            try
    //            {
    //                var complaint = _converterHelper.ToClientComplaint(model, false);

    //                complaint.ModifiedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

    //                model.IsProcessed = true;

    //                await _clientComplaintRepository.UpdateAsync(complaint);

    //                var result = await _userHelper.UpdateUserAsync(user);

    //                if (result.Succeeded)
    //                {
    //                    _mailHelper.SendMail(user.Email, $"Your complaint has been processed.",
    //                   $"<h1>You are very important for us.\nThank you very much.</h1>");
    //                }
    //                else
    //                {
    //                    ViewBag.Message = "An error ocurred. Try again please.";
    //                }
    //                return RedirectToAction(nameof(Complaints));

    //            }
    //            catch (Exception)
    //            {
    //                return new NotFoundViewResult("UserNotFound");
    //            }
    //        }
    //        return View(model);
    //    }

    //    /// <summary>
    //    /// get list of seats to be confirmed
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public async Task<ActionResult> AvailableSeats()
    //    {
    //        var list = await _seatsAvailableRepository.GetSeatsToBeConfirmAsync();

    //        var modelList = new List<AvailableSeatsViewModel>(
    //            list.Select(a => _converterHelper.ToAvailableSeatsViewModel(a))
    //            .ToList());

    //        return View(modelList);
    //    }

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

    //            var result = await _userHelper.UpdateUserAsync(user);


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

    //        var modelList = new List<AdvertisingViewModel>(
    //            list.Select(a => _converterHelper.ToAdvertisingViewModel(a))
    //            .ToList());

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

    //            //user.PendingAdvertising = true;

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