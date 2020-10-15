using MilesBackOffice.Web.Models;

using System.Collections.Generic;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IClientRepository
    {
        List<TierChangeViewModel> GetPendingTierClient();


        List<ComplaintClientViewModel> GetClientComplaint();
    }
}
