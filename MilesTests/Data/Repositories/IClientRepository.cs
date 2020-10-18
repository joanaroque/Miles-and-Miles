using System.Collections.Generic;
using System.Threading.Tasks;
using MilesBackOffice.Web.Models;
using MilesBackOffice.Web.Models.SuperUser;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IClientRepository
    {
        Task<List<TierChangeViewModel>> GetPendingTierClient();



        Task<List<ComplaintClientViewModel>> GetClientComplaints();



        Task<List<AvailableSeatsViewModel>> GetSeatsToBeConfirm();




        Task<List<AdvertisingViewModel>> GetAdvertisingToBeConfirm();

    }
}
