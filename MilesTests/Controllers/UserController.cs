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
        private readonly INotificationHelper _notificationHelper;
        private readonly IImageHelper _imageHelper;


        public UserController(IPremiumRepository premiumRepository,
            IUserHelper userHelper,
            IConverterHelper converter,
            IPartnerRepository partnerRepository,
            IAdvertisingRepository advertisingRepository,
            IFlightRepository flightRepository,
            INotificationHelper notificationHelper,
            IImageHelper imageHelper)
        {
            _premiumRepository = premiumRepository;
            _userHelper = userHelper;
            _converter = converter;
            _partnerRepository = partnerRepository;
            _advertisingRepository = advertisingRepository;
            _flightRepository = flightRepository;
            _notificationHelper = notificationHelper;
            _imageHelper = imageHelper;


        }

        /// <summary>
        /// The list loaded should be only of the results that were sent back by the SU
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


        public async Task<IActionResult> NewsIndex()
        {
            var list = await _advertisingRepository.GetAdvertisingFilteredAsync();
            var modelList = list.Where(st => st.Status == 2).Select(a => _converter.ToAdvertisingViewModel(a));
            return View(modelList);
        }


        public async Task<IActionResult> PartnerIndex()
        {
            var list = await _partnerRepository.GetAllIncludes();

            var modelList = new List<PartnerViewModel>(
               list.Where(st => st.Status == 2)
               .Select(a => _converter.ToPartnerViewModel(a))
               .ToList());

            return View(modelList);
        }

        #region Premium Offers - Create / Edit / Delete

        /***************Create********************/
        [HttpGet]
        public async Task<IActionResult> CreateTicket()
        {
            //var flights = _flightRepository.GetComboFlightList();

            var partners = await _partnerRepository.GetComboPartners();

            var model = new PremiumOfferViewModel
            {
                //Flights = flights,
                PartnersList = partners,
                Type = PremiumType.Ticket
            };

            return PartialView("_CreateTicket", model);
        }


        [HttpGet]
        public async Task<IActionResult> CreateUpgrade()
        {

            //var flights = _flightRepository.GetComboFlightList();

            var partners = await _partnerRepository.GetComboPartners();

            var model = new PremiumOfferViewModel
            {
                //Flights = flights,
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
                var partner = await _partnerRepository.GetByIdAsync(model.PartnerId);

                if (partner == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                var flight = await _flightRepository.GetByIdAsync(model.FlightId);
                if ((model.Type == PremiumType.Ticket || model.Type == PremiumType.Upgrade) && flight == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                var offer = _converter.ToPremiumOfferModel(model, true, partner, flight);
                offer.CreatedBy = await GetCurrentUser();
                offer.CreateDate = DateTime.UtcNow;

                var result = await _premiumRepository.CreateEntryAsync(offer);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_Error404");
                }
                //send notification to superuser
                await _notificationHelper.CreateNotificationAsync(offer.OfferIdGuid, UserType.SuperUser, "", EnumHelper.GetType(offer.Type));

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return RedirectToAction(nameof(PremiumIndex));

        }

        /***************EDIT********************/
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //validate id
            if (id == null)
            {
                return new NotFoundViewResult("_Error404");
                //TODO in case of items we could just send a message
            }
            try
            {
                var item = await _premiumRepository.GetByIdWithIncludesAsync(id.Value);

                if (item == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                var model = _converter.ToPremiumOfferViewModel(item);

                return PartialView("_Edit", model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PremiumOfferViewModel model)
        {
            try
            {
                var current = await _premiumRepository.GetByIdAsync(model.Id);
                if (current == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                var partner = await _partnerRepository.GetByIdAsync(model.Id);

                var flight = await _flightRepository.GetByIdAsync(model.FlightId);

                current.Flight = flight ?? null;
                current.Conditions = string.IsNullOrEmpty(model.Conditions) ? string.Empty : model.Conditions;
                current.Partner = partner;
                current.Quantity = model.Quantity;
                current.Price = model.Price;
                current.Status = 1;
                current.ModifiedBy = await GetCurrentUser();
                current.UpdateDate = DateTime.UtcNow;

                var result = await _premiumRepository.UpdateOfferAsync(current);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_Error404");
                }

                //update notification to SuperUser
                result = await _notificationHelper.UpdateNotificationAsync(current.OfferIdGuid, UserType.SuperUser, "");
                if (!result.Success)
                {
                    //if notification does not exist, creates one
                    await _notificationHelper.CreateNotificationAsync(current.OfferIdGuid, UserType.SuperUser, "", EnumHelper.GetType(current.Type));
                }

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }


        /***************DELETE********************/

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }

                var item = await _premiumRepository.GetByIdWithIncludesAsync(id.Value);
                if (item == null)
                {
                    throw new Exception();
                }

                var result = await _premiumRepository.DeleteAsync(item);
                if (!result)
                {
                    throw new Exception("Failed to delete.");
                }

                var notify = _notificationHelper.DeleteOldByIdAsync(item.OfferIdGuid);

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404");
            }
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
                var partner = _converter.ToPartnerModel(model, true);
                partner.CreatedBy = await GetCurrentUser();
                partner.CreateDate = DateTime.UtcNow;

                var result = await _partnerRepository.AddPartnerAsync(partner);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_Error404");
                }
                //send notification
                await _notificationHelper.CreateNotificationAsync(partner.PartnerGuidId, UserType.SuperUser, "", NotificationType.Partner);

                return RedirectToAction(nameof(PartnerIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }


        /*************EDIT*******************/
        [HttpGet]
        public async Task<IActionResult> EditPartner(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_Error404");
            }

            var item = await _partnerRepository.GetByIdAsync(id.Value);
            if (item == null)
            {
                return new NotFoundViewResult("_Error404");
            }

            var model = _converter.ToPartnerViewModel(item);

            return PartialView("_EditPartner", model);
        }


        [HttpPost]
        public async Task<IActionResult> EditPartner(PartnerViewModel edit)
        {
            try
            {
                var newPartner = await _partnerRepository.GetByIdAsync(edit.Id);
                if (newPartner == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                //TODO take imagefile and convert to Guid
                newPartner.CompanyName = edit.CompanyName;
                newPartner.Address = edit.Address;
                newPartner.Description = edit.Description;
                newPartner.Designation = edit.Designation;
                newPartner.Url = edit.Url;
                newPartner.Status = 1;
                newPartner.ModifiedBy = await GetCurrentUser();
                newPartner.UpdateDate = DateTime.UtcNow;

                var result = await _partnerRepository.UpdatePartnerAsync(newPartner);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_Error404");
                }

                result = await _notificationHelper.UpdateNotificationAsync(newPartner.PartnerGuidId, UserType.SuperUser, "");
                if (!result.Success)
                {
                    await _notificationHelper.CreateNotificationAsync(newPartner.PartnerGuidId, UserType.SuperUser, "", NotificationType.Partner);
                }

                return RedirectToAction(nameof(PartnerIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }


        public async Task<IActionResult> DeletePartner(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }

                var item = await _partnerRepository.GetByIdAsync(id.Value);
                if (item == null)
                {
                    throw new Exception();
                }

                var result = await _partnerRepository.DeleteAsync(item);
                if (!result)
                {
                    throw new Exception("Failed to delete.");
                }

                var notify = _notificationHelper.DeleteOldByIdAsync(item.PartnerGuidId);

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublishPost(AdvertisingViewModel model)
        {
            try
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "advertisings");
                }

                var partner = await _partnerRepository.GetByIdAsync(model.PartnerId);

                if (partner == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                Advertising post = _converter.ToAdvertising(model, true, path, partner);

                //send notification
                await _notificationHelper.CreateNotificationAsync(post.PostGuidId, UserType.SuperUser, "", NotificationType.Advertising);

                post.CreatedBy = await GetCurrentUser();
                post.CreateDate = DateTime.UtcNow;

                await _advertisingRepository.CreateAsync(post);

                return RedirectToAction(nameof(NewsIndex));
            }

            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return RedirectToAction(nameof(NewsIndex));
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("_Error404");
            }
            try
            {
                var advertise = await _advertisingRepository.GetByIdAsync(id.Value);
                if (advertise == null)
                {
                    return new NotFoundViewResult("_Error404");
                }

                var model = _converter.ToAdvertisingViewModel(advertise);


                return PartialView("_EditPost", model);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error500");
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditPost(AdvertisingViewModel model)
        {
            try
            {
                var post = await _advertisingRepository.GetByIdAsync(model.Id);

                post.ModifiedBy = await GetCurrentUser();
                post.UpdateDate = DateTime.UtcNow;

                var result = await _advertisingRepository.UpdatePostAsync(post);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_Error404");
                }

                //notification
                result = await _notificationHelper.UpdateNotificationAsync(post.PostGuidId, UserType.SuperUser, "");
                if (!result.Success)
                {
                    await _notificationHelper.CreateNotificationAsync(post.PostGuidId, UserType.SuperUser, "", NotificationType.Advertising);
                }

                return RedirectToAction(nameof(NewsIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }

        public async Task<IActionResult> DeletePost(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }

                var item = await _advertisingRepository.GetByIdAsync(id.Value);
                if (item == null)
                {
                    throw new Exception();
                }

                var result = await _advertisingRepository.DeleteAsync(item);
                if (!result)
                {
                    throw new Exception("Failed to delete.");
                }

                var notify = _notificationHelper.DeleteOldByIdAsync(item.PostGuidId);

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404");
            }
        }
        #endregion



        private async Task<User> GetCurrentUser()
        {
            return await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
        }


        public async Task<IActionResult> GetFlights(string partnerId)
        {
            var list = await _flightRepository.GetFlightsByPartner(int.Parse(partnerId));

            return Json(list);
        }


        public async Task<IActionResult> GetPrice(int itemId)
        {
            try
            {
                var item = await _flightRepository.GetByIdWithIncludesAsync(itemId);
                if (item == null)
                {
                    throw new Exception();
                }

                var price = item.Distance * 10;

                return Json(price);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
