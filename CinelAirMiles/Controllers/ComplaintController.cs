namespace CinelAirMiles.Controllers
{
    using global::CinelAirMiles.Data.Repositories;
    using global::CinelAirMiles.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Threading.Tasks;

    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IUserHelperClient _userHelper;
        private readonly IClientConverterHelper _converterHelper;

        public ComplaintController(
            IComplaintRepository complaintRepository,
            IUserHelperClient userHelper,
            IClientConverterHelper converterHelper)
        {
            _complaintRepository = complaintRepository;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            var list = _complaintRepository.GetClientComplaints(user);

            return View(list);
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

                    return RedirectToAction(nameof(Index));

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
