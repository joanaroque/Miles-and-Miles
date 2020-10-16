using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;
using System.Collections.Generic;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IClientRepository
    {
        List<TierChangeViewModel> GetPendingTierClient();



        List<ComplaintClientViewModel> GetClientComplaint();



        List<AvailableSeatsViewModel> GetSeatsToBeConfirm();




        List<AdvertisingViewModel> GetAdvertisingToBeConfirm();

    }
}
