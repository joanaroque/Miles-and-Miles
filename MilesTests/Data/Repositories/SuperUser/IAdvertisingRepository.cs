using MilesBackOffice.Web.Data.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        Task<List<Advertising>> GetAdvertisingToBeConfirmAsync();

    }
}
