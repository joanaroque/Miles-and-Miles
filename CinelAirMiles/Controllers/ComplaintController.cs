namespace CinelAirMiles.Controllers
{
    using global::CinelAirMiles.Data.Repositories;
    using global::CinelAirMiles.Helpers;
    using global::CinelAirMiles.Models;
    using Microsoft.AspNetCore.Mvc;
    using MilesBackOffice.Web.Helpers;
    using System;
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

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            var model = new ComplaintViewModel
            {
                Complaints = _complaintRepository.GetComboComplaintTypes()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComplaintViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            //        if (user == null)
            //        {
            //            //todo criar erro
            //        }

            //        var complaint = _converterHelper.ToComplaintClientViewModel(model);

            //        var result = await _complaintRepository.CreateAsync(complaint);

            //        if (!result)
            //        {
            //            //todo criar erro
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError(string.Empty, ex.Message);
            //    }

            //    return RedirectToAction(nameof(Index));
            //}
            return RedirectToAction(nameof(Index));
        }
    }
}
