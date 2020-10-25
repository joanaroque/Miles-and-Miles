namespace CinelAirMiles.Controllers
{
    using global::CinelAirMiles.Data.Repositories;
    using global::CinelAirMiles.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Linq;
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

        public IActionResult Index()
        {
            var client = _complaintRepository.GetAll().ToList();

            return View(client);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var client = await _complaintRepository.GetFirstClientAsync(User.Identity.Name.ToLower());

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
                    var complaint = await _complaintRepository.GetClientWithUserByIdAsync(model.Id);

                    if (complaint == null)
                    {
                        return NotFound();// criar erros

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
