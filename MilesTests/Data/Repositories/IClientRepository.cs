using System.Collections.Generic;

using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;

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
