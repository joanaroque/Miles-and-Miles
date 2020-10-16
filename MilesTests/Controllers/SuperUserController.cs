using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MilesBackOffice.Web.Data;
using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Data.Repositories;
using MilesBackOffice.Web.Helpers;
using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Controllers
{
    public class SuperUserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IClientRepository _clientRepository;

        public SuperUserController(IUserHelper userHelper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IClientRepository clientRepository)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _clientRepository = clientRepository;
        }


        [HttpGet]
        public ActionResult TierChange()
        {
            //lista de clientes com tier pendente
            List<TierChangeViewModel> modelList = _clientRepository.GetPendingTierClient();

            return View(modelList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmTierChange(string clientUserId)
        {
            var user = await _userHelper.GetUserByIdAsync(clientUserId);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            //update to confirm
            

            return View(); // same view
        }

        [HttpGet]
        public ActionResult Complaints()
        {
            //lista reclamaçoes nao processadas
            List<ComplaintClientViewModel> modelList = _clientRepository.GetClientComplaint();


            return View(modelList);
        }

        public async Task<IActionResult> ComplaintReplay(ComplaintClientViewModel model)
        {
            var user = await _userHelper.GetUserByIdAsync(model.Id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View();
        }

        [HttpGet]
        public ActionResult AvailableSeats()
        {
            //lista de de lugares por confirmar
            List<AvailableSeatsViewModel> modelList = _clientRepository.GetSeatsToBeConfirm();


            return View(modelList);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AvailableSeatsModel(string aquiVaiModelclientId)
        {
            var user = await _userHelper.GetUserByIdAsync(aquiVaiModelclientId);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View();
        }

        [HttpGet]
        public ActionResult AdvertisingAndReferences()
        {
            //lista de publicidade por confirmar
            List<AdvertisingViewModel> modelList = _clientRepository.GetAdvertisingToBeConfirm();


            return View(modelList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdvertisingAndReferencesModel(string aquiVaiModelclientId)
        {
            var user = await _userHelper.GetUserByIdAsync(aquiVaiModelclientId);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View();
        }

    }
}