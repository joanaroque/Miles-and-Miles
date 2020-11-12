﻿namespace CinelAirMiles.Controllers
{
    using CinelAirMiles.Helpers;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using global::CinelAirMiles.Models;

    using Microsoft.AspNetCore.Mvc;
    using MilesBackOffice.Web.Helpers;
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
                        return new NotFoundViewResult("_Error404Client");
                    }

                    var list = await _transactionRepository.GetAllByClient(user);

                    var modelList = new List<TransactionViewModel>(
                        list.Select(c => _converterHelper.ToTransactionViewModel(c, user))
                        .ToList());

                    return View(modelList);
                }

                else
                {
                    return new NotFoundViewResult("_Error404Client");
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
        public async Task<IActionResult> Purchase(TransactionViewModel model)
        {
            //TODO blocos de 2000 milhas
            try
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
                if (user == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }

                var value = 2000;

                model = new TransactionViewModel
                {
                    Value = value,
                    Price = _transactionHelper.MilesPrice(value)
                };

                return PartialView("_Purchase", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Purchase(Transaction transaction, User user)
        {
            user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

            _transactionHelper.NewPurchase(transaction, user);

            var result = await _transactionRepository.CreateAsync(transaction);

            if (!result)
            {
                ModelState.AddModelError(string.Empty,
                    "An error ocurred while submitting your request. Please try again.");

                return PartialView("_Purchase");
            }

            user.BonusMiles = transaction.EndBalance;

            var result2 = await _userHelper.UpdateUserAsync(user);

            if (result2.Succeeded)
            {
                ModelState.AddModelError(string.Empty,
                    "You purchase was successfull. Your balance should reflect it in the next hours.");
            }

            return PartialView("_Purchase");
        }


        [HttpGet]
        public IActionResult ExtendMiles()
        {
            //TODO blocos de 2000 milhas e max de 20 000 milhas por ano (fazer validação)

           

            return PartialView("_ExtendMiles");
        }


        //[HttpPost]
        //public IActionResult ExtendMiles()
        //{
        //    TODO
        //    validas durante 3 anos

        //     é apenas possível para as milhas que estão a caducar
        //    na sua próxima data de caducidade de milhas ????? = as proximas na fila, a caducar?


        //    return PartialView("_ExtendMiles");
        //}


        [HttpGet]
        public async Task<IActionResult> TransferMiles(TransactionViewModel model)
        {
            //TODO blocos de 2000 milhas

            try
            {
                var user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);
                if (user == null)
                {
                    return new NotFoundViewResult("_Error404Client");
                }

                model = new TransactionViewModel
                {
                    Value = 2000,
                    Price = 10
                };

                return PartialView("_TransferMiles", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TransferMiles(Transaction transaction, User user, User userTo)
        {
            //TODO
            //validas por 1 ano

            //transf de status, bonus ou as duas?? passam para bonus!


            user = await _userHelper.GetUserByUsernameAsync(User.Identity.Name);

            userTo = _userHelper.GetUserByGuidId(transaction.TransferTo.GuidId);

            _transactionHelper.NewTransfer(transaction, user, userTo);

            var result = await _transactionRepository.CreateAsync(transaction);

            if (!result)
            {
                ModelState.AddModelError(string.Empty,
                    "An error ocurred while submitting your request. Please try again.");

                return PartialView("_TransferMiles");
            }

            user.BonusMiles = transaction.EndBalance;

            var result2 = await _userHelper.UpdateUserAsync(user);

            userTo.BonusMiles = userTo.BonusMiles + transaction.Value;

            var result3 = await _userHelper.UpdateUserAsync(userTo);

            if (result2.Succeeded && result3.Succeeded)
            {
                ModelState.AddModelError(string.Empty,
                    "You transfer was successfull. Your balance should reflect it in the next hours.");
            }

            else
            {
                ModelState.AddModelError(string.Empty,
                   "An error ocurred while submitting your request. Please try again.");
            }

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
            //TODO
            //apenas clientes silver ou basic
            
            return PartialView("_NominateToGold");
        }


    }
}
