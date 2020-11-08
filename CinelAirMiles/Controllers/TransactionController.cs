namespace CinelAirMiles.Controllers
{
    using CinelAirMiles.Helpers;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TransactionController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientConverterHelper _converterHelper;
        private readonly ITransactionHelper _transactionHelper;

        public TransactionController(
            IUserHelper userHelper,
            ITransactionRepository transactionRepository,
            IClientConverterHelper converterHelper,
            ITransactionHelper transactionHelper)
        {
            _userHelper = userHelper;
            _transactionRepository = transactionRepository;
            _converterHelper = converterHelper;
            _transactionHelper = transactionHelper;
        }

        [HttpGet]
        public async Task<IActionResult> TransactionIndex()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    var list = await _transactionRepository.GetAllByClient(user);

                    var modelList = new List<TransactionViewModel>(
                        list.Select(c => _converterHelper.ToTransactionViewModel(c, user))
                        .ToList());

                    return View(modelList);
                }

                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View();
        }


        public IActionResult MilesIndex()
        {
            //todo or to delete ?
            return View();
        }


        [HttpGet]
        public IActionResult Purchase(TransactionViewModel model)
        {
            //TODO blocos de 2000 milhas
            
            var value = 2000;

            model = new TransactionViewModel
            {
                Value = value,
                Price = _transactionHelper.MilesPrice(value)
            };

            return PartialView("_Purchase", model);
        }



        [HttpPost]
        public IActionResult Purchase()
        {
            //ask quantity


            //math for the price in €


            //check if balance is enough


            //client confirms and goes to confirmation page? confirmation pop up?


            //add miles to user







            return PartialView("_Purchase");
        }




        [HttpGet]
        public IActionResult ExtendMiles()
        {
            //TODO blocos de 2000 milhas

            return PartialView("_ExtendMiles");
        }


        [HttpGet]
        public IActionResult TransferMiles()
        {
            //TODO blocos de 2000 milhas
            //com o guid id
            return PartialView("_TransferMiles");
        }


        [HttpGet]
        public IActionResult ConvertMiles()
        {
            //TODO blocos de 2000 milhas

            return PartialView("_ConvertMiles");
        }


        [HttpGet]
        public IActionResult NominateToGold()
        {
            //com o guid id
            return PartialView("_NominateToGold");
        }


    }
}
