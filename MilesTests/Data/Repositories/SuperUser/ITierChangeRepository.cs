using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    public interface ITierChangeRepository : IGenericRepository<TierChange>
    {
        Task<List<TierChange>> GetPendingTierClientListAsync();


        Task<TierChange> GetByIdWithIncludesAsync(int id);
    }
}
