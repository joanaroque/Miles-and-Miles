namespace MilesBackOffice.Web.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Data.Repositories.User;
    using MilesBackOffice.Web.Helpers;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.User;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converter;
        private readonly IPartnerRepository _partnerRepository;
        private readonly INewsRepository _newsRepository;

        public UserController(IPremiumRepository premiumRepository,
            IUserHelper userHelper,
            IConverterHelper converter,
            IPartnerRepository partnerRepository,
            INewsRepository newsRepository)
        {
            _premiumRepository = premiumRepository;
            _userHelper = userHelper;
            _converter = converter;
            _partnerRepository = partnerRepository;
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// The list loaded should be only of the results that were sent back by the SU
        /// TODO The page should include a button that loads the entire datacontext of PremiumOffers as demand of the User
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PremiumIndex()
        {
            var model = new PremiumIndexViewModel
            {
                PremiumOffers = await _premiumRepository.GetAllOffersAsync()
            };

            return View(model);
        }


        public IActionResult NewsIndex()
        {
            var model = new NewsViewModel
            {
                Newspaper = _newsRepository.GetAll()
            };

            return View(model);
        }


        public IActionResult PartnerIndex()
        {
            var model = new PartnerViewModel
            {
                Partners = _partnerRepository.GetAll()
            };

            return View(model);
        }

        #region Premium Offers - Create / Edit / Delete

        /***************Create********************/
        [HttpGet]
        public async Task<IActionResult> CreateTicket()
        {
            //tests
            var flights = new List<SelectListItem>();
            flights.Add(new SelectListItem
            {
                Text = "F192LISOPO121120",
                Value = "1"
            });

            var partners = await _partnerRepository.GetComboPartners();


            var model = new CreateTicketViewModel
            {
                Flights = flights,
                PartnersList = partners
            };

            return PartialView("_CreateTicket", model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketViewModel model)
        {
            try
            {

                //var user = await GetUserByName();
                //if (user == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                //converts the model into the proper class
                var ticket = _converter.ToPremiumTicket(model);
                //ticket.CreatedBy = user;
                ticket.CreateDate = DateTime.UtcNow;

                var result = await _premiumRepository.CreateEntryAsync(ticket);

                if (!result.Success)
                {
                    //TODO Error DataContext
                    return new NotFoundViewResult("_DatacontextError");
                }

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                //TODO 500 ERROR
                return new NotFoundViewResult("_500Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateUpgrade()
        {
            //tests
            var flights = new List<SelectListItem>();
            flights.Add(new SelectListItem
            {
                Text = "F192LISOPO121120",
                Value = "1"
            });

            var partners = await _partnerRepository.GetComboPartners();

            var model = new CreateUpgradeViewModel
            {
                Flights = flights,
                PartnersList = partners
            };

            return PartialView("_CreateUpgrade", model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUpgrade(CreateUpgradeViewModel model)
        {
            try
            {
                //var user = await GetUserByName();
                //if (user == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var upgrade = _converter.ToPremiumUpgrade(model);
                //upgrade.CreatedBy = user;
                upgrade.CreateDate = DateTime.UtcNow;

                var result = await _premiumRepository.CreateEntryAsync(upgrade);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }
                //send notification to SU ? Notification.cs

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateVoucher()
        {
            var partners = await _partnerRepository.GetComboPartners();

            var model = new CreateVoucherViewModel
            {
                PartnersList = partners
            };

            return PartialView("_CreateVoucher", model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateVoucher(CreateVoucherViewModel model)
        {
            try
            {
                //var user = await GetUserByName();
                //if (user == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var voucher = _converter.ToPremiumVoucher(model);
                //voucher.CreatedBy = user;
                voucher.CreateDate = DateTime.UtcNow;

                var result = await _premiumRepository.CreateEntryAsync(voucher);

                if (!result.Success)
                {
                    return new NotFoundViewResult("_DataContextError");
                }
                //send notification to SU ? Notification.cs

                return RedirectToAction(nameof(PremiumIndex));
            }
            catch (Exception)
            {
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
                return new NotFoundViewResult("_404NotFound");//TODO in case of items we could just send a message
            }
            try
            {
                //retrieve entry from DB
                var item = await _premiumRepository.GetByIdAsync(id.Value);

                return PartialView("_Edit", item);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PremiumOffer edit)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var current = await _premiumRepository.GetByIdAsync(edit.Id);
                if (current == null)
                {
                    //TODO ITEMNOTFOUND ERROR
                    return new NotFoundViewResult("_ItemNotFound");
                }

                await _converter.UpdateOfferAsync(current, edit);
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
        public async Task<IActionResult> AddNewPartner(CreatePartnerViewModel model)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var partner = _converter.ToPartnerModel(model);
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
                //or error on id is null
                return new NotFoundViewResult("_ItemNotFound");
            }

            var item = await _partnerRepository.GetByIdAsync(id.Value);
            if (item == null)
            {
                return new NotFoundViewResult("_ItemNotFound");
            }

            return PartialView("_EditPartner", item);
        }


        [HttpPost]
        public async Task<IActionResult> EditPartner(Partner edit)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var partner = await _partnerRepository.GetByIdAsync(edit.Id);
                if (partner == null)
                {
                    return new NotFoundViewResult("_ItemNotFound");
                }

                await _converter.UpdatePartnerAsync(partner, edit);
                //partner.ModifiedBy = currentUser;
                partner.UpdateDate = DateTime.UtcNow;

                var result = await _partnerRepository.UpdatePartnerAsync(partner);

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
        public IActionResult PublishPost()
        {
            return PartialView("_PublishPost");
        }


        [HttpPost]
        public async Task<IActionResult> PublishPost(PublishNewsViewModel model)
        {
            try
            {
                //var currentUser = await GetUserByName();
                //if (currentUser == null)
                //{
                //    return new NotFoundViewResult("_UserNotFound");
                //}

                var post = _converter.ToNewsModel(model);
                //post.CreatedBy = currentUser;
                post.CreateDate = DateTime.UtcNow;

                var result = await _newsRepository.CreatePostAsync(post);

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
                var item = await _newsRepository.GetByIdAsync(id.Value);
                if (item == null)
                {
                    return new NotFoundViewResult("_ItemNotFound");
                }

                return PartialView("_EditPost", item);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_500Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditPost(News model)
        {
            try
            {
                var currentUser = await GetUserByName();
                if (currentUser == null)
                {
                    return new NotFoundViewResult("_UserNotFound");
                }

                var post = await _newsRepository.GetByIdAsync(model.Id);

                await _converter.UpdatePostAsync(post, model);
                //post.ModifiedBy = currentUser;
                post.UpdateDate = DateTime.UtcNow;

                var result = await _newsRepository.UpdatePostAsync(post);

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
