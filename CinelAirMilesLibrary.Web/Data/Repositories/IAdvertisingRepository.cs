namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        Task<List<Advertising>> GetAdvertisingForClientAsync();


        Task<Response> CreatePostAsync(Advertising item);


        /// <summary>
        /// get a list of advertising, including the partner name and created by 
        /// </summary>
        /// <returns>list of advertising</returns>
        Task<List<Advertising>> GetAdvertisingFilteredAsync();



        Task<List<Advertising>> GetAdvertisingApproved();


        /// <summary>
        /// get the first or default advertising, including who created, and partner name
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>an advertising </returns>
        Task<Advertising> GetByIdWithIncludesAsync(int id);




        Task<Response> UpdatePostAsync(Advertising item);



        Task<List<Advertising>> GetAllIncludes();
    }
}
