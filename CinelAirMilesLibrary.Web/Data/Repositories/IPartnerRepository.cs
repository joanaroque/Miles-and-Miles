namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using Microsoft.AspNetCore.Mvc.Rendering;


    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        Task<IEnumerable<SelectListItem>> GetComboAirlines();


        /// <summary>
        /// Asynchronously adds an entry to context
        /// </summary>
        /// <param name="model">the entry to be added</param>
        /// <returns>A response with the result of the operation, or an error message if it fails</returns>
        Task<Response> AddPartnerAsync(Partner model);

        IEnumerable<SelectListItem> GetComboPartnerTypes();


        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list with all Partners</returns>
        Task<IEnumerable<SelectListItem>> GetComboPartners();


        /// <summary>
        /// Asynchronously updates an entry to context
        /// </summary>
        /// <param name="model">the entry to be added</param>
        /// <returns>A response with the result of the operation, or an error message if it fails</returns>
        Task<Response> UpdatePartnerAsync(Partner model);




        /// <summary>
        /// get the first or default partner, including who created
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>an partner </returns>
        Task<Partner> GetByIdWithIncludesAsync(int id);



        /// <summary>
        /// get a list of advertising, including the partner name
        /// </summary>
        /// <returns>list of advertising</returns>
        Task<List<Partner>> GetPartnerWithStatus1Async();

        Task<List<Partner>> GetPartnerWithStatus0Async();


        Task<List<Partner>> GetAllIncludes();

    }
}
