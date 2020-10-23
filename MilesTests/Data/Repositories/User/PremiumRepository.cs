namespace MilesBackOffice.Web.Data.Repositories.User
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using Microsoft.EntityFrameworkCore;

    using MilesBackOffice.Web.Helpers;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PremiumRepository : GenericRepository<PremiumOffer>, IPremiumRepository
    {
        private readonly DataContext _dataContext;

        public PremiumRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<Response> CreateEntryAsync(PremiumOffer item)
        {
            try
            {
                //adds entry
                var result = await CreateAsync(item);

                if (result)
                {
                    return new Response
                    {
                        Success = true
                    };
                }
                return new Response
                {
                    //returns true, false if nothing was saved
                    Success = false,
                    Message = "Your data was not saved. Please try again.\n If the problem persists contact an Administrator."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response> UpdateOfferAsync(PremiumOffer model)
        {
            try
            {
                var result = await UpdateAsync(model);

                if (result)
                {
                    return new Response
                    {
                        Success = true
                    };
                }
                return new Response
                {
                    Success = false,
                    Message = "The update was not saved.Please try again.\n If the problem persists contact an Administrator."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<PremiumOffer> GetByIdWithIncludesAsync(int id)
        {
            var seatsAvailable = await _dataContext.PremiumOffers
                  .Include(t => t.CreatedBy)
                  .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return seatsAvailable;
        }

        public async Task<List<PremiumOffer>> GetAllOffersAsync()
        {
            return await _dataContext.PremiumOffers.ToListAsync();
        }
    }
}
