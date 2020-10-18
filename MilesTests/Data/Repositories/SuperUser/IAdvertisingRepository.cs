using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Helpers;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        Task<List<Advertising>> GetAdvertisingToBeConfirmAsync();



        Task<Advertising> GetByIdWithIncludesAsync(int id);

    }
}
