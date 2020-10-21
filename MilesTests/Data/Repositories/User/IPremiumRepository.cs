﻿namespace MilesBackOffice.Web.Data.Repositories.User
{
    using MilesBackOffice.Web.Data.Repositories;
    using MilesBackOffice.Web.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MilesBackOffice.Web.Helpers;

    public interface IPremiumRepository : IGenericRepository<PremiumOffer>
    {
        /// <summary>
        /// Creates a new entry in DataContext and saves the tracked modification
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A Response with the success of the operation, otherwise throws an error</returns>
        Task<Response> CreateEntryAsync(PremiumOffer item);

       
        Task<Response> UpdateOfferAsync(PremiumOffer model);



        /// <summary>
        /// Gets all entries in DataContext
        /// </summary>
        /// <returns></returns>
        Task<List<PremiumOffer>> GetAllOffersAsync();


        Task<PremiumOffer> GetByIdWithIncludesAsync(int id);
    }
}
