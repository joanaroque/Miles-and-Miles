namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;

    public class UserController : Controller
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converter;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IFlightRepository _flightRepository;

        public UserController(IPremiumRepository premiumRepository,
            IUserHelper userHelper,
            IConverterHelper converter,
            IPartnerRepository partnerRepository,
            IAdvertisingRepository advertisingRepository,
            IFlightRepository flightRepository)
        {
            _premiumRepository = premiumRepository;
            _userHelper = userHelper;
            _converter = converter;
            _partnerRepository = partnerRepository;
            _advertisingRepository = advertisingRepository;
            _flightRepository = flightRepository;
        }

        /// <summary>
        /// The list loaded should be only of the results that were sent back by the SU
        /// TODO The page should include a button that loads the entire datacontext of PremiumOffers as demand of the User
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PremiumIndex()
        {
            var list = await _premiumRepository.GetAllIncludes();

            var modelList = new List<PremiumOfferViewModel>(
                list.Where(st => st.Status == 2)
                .Select(a => _converter.ToPremiumOfferViewModel(a))
                .ToList());

            return View(modelList);

        }


        public async Task<ActionResult> NewsIndex()
        {
            var list = await _advertisingRepository.GetAllAdvertisingAsync();

            var modelList = new List<AdvertisingViewModel>(
                list.Select(a => _converter.ToAdvertisingViewModel(a))
                .ToList());

            return View(modelList);
        }


        public IActionResult PartnerIndex()
        {
            var list = _partnerRepository.GetAll();

            var modelList = new List<PartnerViewModel>(
                list.Select(a => _converter.ToPartnerViewModel(a))
                .ToList());

            return View(modelList);
        }

        #region Premium Offers - Create / Edit / Delete

        /***************Create********************/
        [HttpGet]
        public async Task<IActionResult> CreateTicket()
        {
            var flights = _flightRepository.GetComboFlightList();

            var partners = await _partnerRepository.GetComboPartners();

            var model = new PremiumOfferViewModel
            {
                Flights = flights,
                PartnersList = partners,
                Type = PremiumType.Ticket
            };

            return PartialView("_CreateTicket", model);
        }


        [HttpGet]
        public async Task<IActionResult> CreateUpgrade()
        {

            var flights = _flightRepository.GetComboFlightList();

            var partners = await _partnerRepository.GetComboPartners();

            var model = new PremiumOfferViewModel
            {
                Flights = flights,
                PartnersList = partners,
                Type = PremiumType.Upgrade
            };

            return PartialView("_CreateUpgrade", model);
        }



        [HttpGet]
        public async Task<IActionResult> CreateVoucher()
        {
            var partners = await _partnerRepository.GetComboPartners();

            var model = new PremiumOfferViewModel
            {
                PartnersList = partners,
                Type = PremiumType.Voucher
            };

            return PartialView("_CreateVoucher", model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOffer(PremiumOfferViewModel model)
        {
            try
            {
                //comentado para testes de desenvolvimento
                //var user = await GetUserByName();
                //if (user == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var partner = await _partnerRepository.GetByIdAsync(model.PartnerId);

                if (partner == null)
                {
                    return new NotFoundViewResult("_PartnerNotFound");
                }

                var flight = await _flightRepository.GetByIdAsync(model.FlightId);
                if ((model.Type == PremiumType.Ticket || model.Type == PremiumType.Upgrade) && flight == null)
                {
                    return new NotFoundViewResult("_FlightNotFound");
                }

                var ticket = _converter.ToPremiumOfferModel(model, true, partner, flight);
                //ticket.CreatedBy = user;
                ticket.CreateDate = DateTime.UtcNow;

                var result = await _premiumRepository.CreateEntryAsync(ticket);

                if (!result.Success)
                {
                    //TODO Error DataContext
                    return new NotFoundViewResult("_DatacontextError");
                }

                TempData["Message"] = "Offer was created with success";
                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                //TODO 500 ERROR
                return new NotFoundViewResult("_500Error");
            }
        }


        /***************EDIT********************/
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //validate id
            if (id == null)
            {
                return new NotFoundViewResult("_404NotFound");
                //TODO in case of items we could just send a message
            }
            try
            {
                var item = await _premiumRepository.GetByIdAsync(id.Value);

                if (item == null)
                {
                    return new NotFoundViewResult("_404NotFound");
                }

                var model = _converter.ToPremiumOfferViewModel(item);

                return PartialView("_Edit", model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PremiumOfferViewModel model)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var current = await _premiumRepository.GetByIdAsync(model.Id);
                if (current == null)
                {
                    //TODO ITEMNOTFOUND ERROR
                    return new NotFoundViewResult("_ItemNotFound");
                }

                var partner = await _partnerRepository.GetByIdAsync(model.Id);

                var flight = await _flightRepository.GetByIdAsync(model.FlightId);

                current.Flight = flight ?? null;
                current.Conditions = string.IsNullOrEmpty(model.Conditions) ? string.Empty : model.Conditions;
                current.Partner = partner;
                current.Quantity = model.Quantity;
                current.Price = model.Price;
                current.Status = 1;
                //current.ModifiedBy = currentUser;
                current.UpdateDate = DateTime.UtcNow;

                var result = await _premiumRepository.UpdateOfferAsync(current);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }


        /***************DELETE********************/

        public IActionResult Delete(int? id)
        {
            //delete or status 5 == deleted??
            return RedirectToAction("PremiumIndex");
        }
        #endregion


        #region PartnerShips - Create / Edit / Delete

        public IActionResult AddNewPartner()
        {
            return PartialView("_AddNewPartner");
        }

        /*****************CREATE******************/
        [HttpPost]
        public async Task<IActionResult> AddNewPartner(PartnerViewModel model)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var partner = _converter.ToPartnerModel(model, true);
                //partner.CreatedBy = currentUser;
                partner.CreateDate = DateTime.UtcNow;

                var result = await _partnerRepository.AddPartnerAsync(partner);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }

                return RedirectToAction(nameof(PartnerIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }


        /*************EDIT*******************/
        [HttpGet]
        public async Task<IActionResult> EditPartner(int? id)
        {
            if (id == null)
            {
                //TODO error on id is null
                return new NotFoundViewResult("_ItemNotFound");
            }

            var item = await _partnerRepository.GetByIdAsync(id.Value);
            if (item == null)
            {
                return new NotFoundViewResult("_ItemNotFound");
            }

            var model = _converter.ToPartnerViewModel(item);

            return PartialView("_EditPartner", model);
        }


        [HttpPost]
        public async Task<IActionResult> EditPartner(PartnerViewModel edit)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var current = await _partnerRepository.GetByIdAsync(edit.Id);
                if (current == null)
                {
                    return new NotFoundViewResult("_ItemNotFound");
                }

                //TODO take imagefile and convert to Guid
                current.CompanyName = edit.CompanyName;
                current.Address = edit.Address;
                current.Description = edit.Description;
                current.Designation = edit.Designation;
                current.Url = edit.Url;
                current.Status = 1;
                //partner.ModifiedBy = currentUser;
                current.UpdateDate = DateTime.UtcNow;

                var result = await _partnerRepository.UpdatePartnerAsync(current);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }

                return RedirectToAction(nameof(PartnerIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }
        #endregion


        #region NewsFeed - Create / Edit / Delete

        [HttpGet]
        public async Task<IActionResult> PublishPost()
        {
            var partners = await _partnerRepository.GetComboPartners();

            var model = new AdvertisingViewModel
            {
                PartnersList = partners,
            };

            return PartialView("_PublishPost", model);
        }


        [HttpPost]
        public async Task<IActionResult> PublishPost(AdvertisingViewModel model)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                //TODO deal with the image file

                var partner = await _partnerRepository.GetByIdAsync(int.Parse(model.PartnerName));

                if (partner == null)
                {
                    return new NotFoundViewResult("_PartnerNotFound");
                }

                var post = _converter.ToAdvertising(model, model.ImageId, partner, true);
                //post.CreatedBy = currentUser;
                post.CreateDate = DateTime.UtcNow;

                var result = await _advertisingRepository.CreatePostAsync(post);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }

                return RedirectToAction(nameof(NewsIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_ItemNotFound");
            }
            try
            {
                var advertise = await _advertisingRepository.GetByIdAsync(id.Value);
                if (advertise == null)
                {
                    return new NotFoundViewResult("_ItemNotFound");
                }

                var model = _converter.ToAdvertisingViewModel(advertise);


                return PartialView("_EditPost", model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditPost(AdvertisingViewModel model)
        {
            try
            {
                var currentUser = await GetUserByName();
                if (currentUser == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                var post = await _advertisingRepository.GetByIdAsync(model.Id);




                //post.ModifiedBy = currentUser;
                post.UpdateDate = DateTime.UtcNow;

                var result = await _advertisingRepository.UpdatePostAsync(post);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }

                return RedirectToAction(nameof(NewsIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }
        #endregion



        private async Task<User> GetUserByName()
        {
            return await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
        }
    }
}
