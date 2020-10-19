namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Helpers;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISeatsAvailableRepository : IGenericRepository<SeatsAvailable>
    {
        Task<List<SeatsAvailable>> GetAllSeatsAsync();


        Task<SeatsAvailable> GetByIdWithIncludesAsync(int id);

    }
}
