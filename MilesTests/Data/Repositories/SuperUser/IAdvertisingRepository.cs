namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Helpers;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        Task<Response> CreatePostAsync(Advertising item);
        Task<List<Advertising>> GetAllAdvertisingAsync();



        Task<Advertising> GetByIdWithIncludesAsync(int id);
        Task<Response> UpdatePostAsync(Advertising item);
    }
}
