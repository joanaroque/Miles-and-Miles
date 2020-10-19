namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using MilesBackOffice.Web.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        Task<List<Advertising>> GetAllAdvertisingAsync();



        Task<Advertising> GetByIdWithIncludesAsync(int id);

    }
}
