namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.EntityFrameworkCore;

    public class PremiumRepository : GenericRepository<PremiumOffer>, IPremiumRepository
    {
        private readonly DataContext _dataContext;


        public PremiumRepository(DataContext dataContext)
            : base(dataContext)
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
                //fails to add
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
                  .Include(f => f.Flight)
                  .Include(p => p.Partner)
                  .Include(t => t.CreatedBy)
                  .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return seatsAvailable;
        }

        public async Task<List<PremiumOffer>> GetAllOffersAsync()
        {
            return await _dataContext.PremiumOffers.ToListAsync();
        }

        public async Task<IEnumerable<PremiumOffer>> GetAllIncludes()
        {
            return await _dataContext.PremiumOffers
                .Include(p => p.Partner)
                .Include(u => u.CreatedBy)
                .Include(f => f.Flight)
                .ToListAsync();
        }


        public async Task<IEnumerable<PremiumOffer>> GetAllInclundedWithStatus(int status)
        {
            return await Task.Run(() => _dataContext.PremiumOffers
                .Include(p => p.Partner)
                .Include(u => u.CreatedBy)
                .Where(st => st.Status == status)
                .AsNoTracking());
        }


        public IEnumerable<PremiumOffer> SearchByParameters(string departure, string arrival)
        {
            //search only by destination
            if (string.IsNullOrWhiteSpace(arrival))
            {
                return _dataContext.PremiumOffers
                    .Include(f => f.Flight)
                    .Include(p => p.Partner)
                    .Where(fi => fi.Flight.Departure.Equals(departure) && fi.Flight.DepartureDate.Date > DateTime.Now.Date);
            }
            else
            {
                return _dataContext.PremiumOffers
                    .Include(f => f.Flight)
                    .Include(p => p.Partner)
                    .Where(fi => fi.Flight.Departure.Equals(departure) && fi.Flight.Arrival.Equals(arrival) && fi.Flight.DepartureDate.Date >= DateTime.Now.Date);
            }
        }


    }
}