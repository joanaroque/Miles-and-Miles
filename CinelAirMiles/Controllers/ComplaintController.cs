namespace CinelAirMiles.Controllers
{
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using global::CinelAirMiles.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;

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
                    return NotFound();
                }

                var list = await _complaintRepository.GetClientComplaintsAsync(user.Id);

                var modelList = new List<ComplaintViewModel>(
                    list.Select(c => _converterHelper.ToComplaintClientViewModel(c))
                    .ToList());
            }

            else
            {
                return NotFound();
            }

            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            var client = User.Identity.Name;

            if (client == null)
            {
                return NotFound();
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
                    var complaint = await _complaintRepository.GetClientByIdAsync(model.Id);

                    if (complaint == null)
                    {
                        return NotFound();
                    }

                    var user = await _userHelper.GetUserByIdAsync(complaint.CreatedBy.Id);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    var clientComplaint = _converterHelper.ToClientComplaint(model, true);
                    clientComplaint.CreatedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

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
    }
}
