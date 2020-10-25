namespace CinelAirMiles.Controllers
{
    using global::CinelAirMiles.Data.Repositories;
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



        /// <summary>
        /// get list of all clients and transforms an entity to viewmodel
        /// </summary>
        /// <returns>the list of all clients</returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var list = await _complaintRepository.GetAllClientListAsync(); // mysteriously, the enum started to work

            var modelList = new List<ComplaintViewModel>(
                list.Select(a => _converterHelper.ToComplaintClientViewModel(a))
                .ToList());

            return View(modelList);
 
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
