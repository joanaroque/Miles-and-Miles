namespace MilesBackOffice.Web.Data.Repositories.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Helpers;

    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        /// <summary>
        /// Asynchronously adds an entry to context
        /// </summary>
        /// <param name="model">the entry to be added</param>
        /// <returns>A response with the result of the operation, or an error message if it fails</returns>
        Task<Response> AddPartnerAsync(Partner model);


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
    }
}
