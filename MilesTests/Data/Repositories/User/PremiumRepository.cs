namespace MilesBackOffice.Web.Data.Repositories.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Helpers;

    public class PremiumRepository : GenericRepository<PremiumOffer>, IPremiumRepository
    {
        private readonly DataContext _dataContext;

        public PremiumRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }


        public IEnumerable<PremiumOffer> GetAllOffers()
        {
            return _dataContext.PremiumOffers.AsNoTracking();
        }


        public async Task<Response> CreateEntryAsync(PremiumOffer item)
        {
            try
            {
                //adds entry
                _dataContext.PremiumOffers.Add(item);

                //gets the result(int) of the save operation
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
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

    }
}
