namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using MilesBackOffice.Web.Helpers;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        Task<Response> CreatePostAsync(Advertising item);


        /// <summary>
        /// get a list of advertising, including the partner name
        /// </summary>
        /// <returns>list of advertising</returns>
        Task<List<Advertising>> GetAllAdvertisingAsync();



        /// <summary>
        /// get the first or default advertising, including who created, and partner name
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>an advertising </returns>
        Task<Advertising> GetByIdWithIncludesAsync(int id);



        Task<Response> UpdatePostAsync(Advertising item);
    }
}
