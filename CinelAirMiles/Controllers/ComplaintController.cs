namespace CinelAirMiles.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CinelAirMiles.Helpers;
    using CinelAirMiles.Models;

    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc;

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
                    var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                    if (user == null)
                    {
                        return new NotFoundViewResult("_Error404Client");
                    }

                    var clientComplaint = _converterHelper.ToClientComplaint(model, true, user);

                    await _complaintRepository.CreateAsync(clientComplaint);

                    return RedirectToAction(nameof(ClientAreaController.ComplaintsIndex), nameof(ClientAreaController));

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
