using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IClientComplaintRepository : IGenericRepository<ClientComplaint>
    {
        Task<List<ClientComplaint>> GetClientComplaintsAsync();



        Task<ClientComplaint> GetByIdWithIncludesAsync(int id);

    }
}
