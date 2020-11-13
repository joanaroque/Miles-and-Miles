namespace MilesBackOffice.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Repositories;
    using CinelAirMilesLibrary.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;

    using MilesBackOffice.Web.Helpers;

    public class ClientController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IPremiumRepository _premiumRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ITransactionRepository _transactionRepository;

        public ClientController(IComplaintRepository complaintRepository,
                IPremiumRepository premiumRepository,
                IConverterHelper converterHelper,
                ITransactionRepository transactionRepository)
        {
            _complaintRepository = complaintRepository;
            _premiumRepository = premiumRepository;
            _converterHelper = converterHelper;
            _transactionRepository = transactionRepository;
        }

        //TODO more methods

        [HttpGet]
        public async Task<IActionResult> GetClientComplaints(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception();
                }

                var result = await _complaintRepository.GetClientComplaintsAsync(id);

                var list = result.Select(item => _converterHelper.ToComplaintClientViewModel(item));

                return PartialView("_ComplaintIndex", list);
            }
            catch (Exception)
            {
                return new NotFoundViewResult("_Error404");
            }
        }

    }
}
