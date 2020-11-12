namespace CinelAirMiles.Controllers
{
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using global::CinelAirMiles.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;
    using MilesBackOffice.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IUserHelper _userHelper;
        private readonly IClientConverterHelper _converterHelper;

        public ComplaintController(
            IComplaintRepository complaintRepository,
            IUserHelper userHelper,
            IClientConverterHelper converterHelper)
        {
            _complaintRepository = complaintRepository;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }


        [HttpGet]
        public async Task<ActionResult> ComplaintsIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                if (user == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }

                var list = await _complaintRepository.GetClientComplaintsAsync(user.Id);

                var modelList = new List<ComplaintViewModel>(
                    list.Select(c => _converterHelper.ToComplaintClientViewModel(c))
                    .ToList());

                if (modelList.Count == 0)
                {
                    return new NotFoundViewResult("_Error404Client");
                }

                return View(modelList);
            }

            else
            {
                return new NotFoundViewResult("_Error404Client");
            }

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
                    // ?? var complaint = await _complaintRepository.GetClientByIdAsync(model.Id);

                    //if (complaint == null)
                    //{
                    //    return new NotFoundViewResult("_Error404Client");
                    //}

                    //var user = await _userHelper.GetUserByIdAsync(complaint.CreatedBy.Id);

                    var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                    if (user == null)
                    {
                        return new NotFoundViewResult("_Error404Client");
                    }

                    var clientComplaint = _converterHelper.ToClientComplaint(model, true, user);

                    await _complaintRepository.CreateAsync(clientComplaint);

                    return RedirectToAction(nameof(ComplaintsIndex));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            var user = _userHelper.GetUserByUsernameAsync(User.Identity.Name);

            var complaint = await _complaintRepository.GetByIdAsync(id.Value);

            if (complaint == null)
            {
                return new NotFoundViewResult("_Error404Client");
            }

            //var model = await _converterHelper.ToClientComplaint(complaint, false, user);

            //return View(model);

            return View();
        }
    }
}
