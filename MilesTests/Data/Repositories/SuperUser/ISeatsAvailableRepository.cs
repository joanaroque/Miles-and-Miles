using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    public interface ISeatsAvailableRepository : IGenericRepository<SeatsAvailable>
    {
        Task<List<SeatsAvailable>> GetSeatsToBeConfirmAsync();

    }
}
