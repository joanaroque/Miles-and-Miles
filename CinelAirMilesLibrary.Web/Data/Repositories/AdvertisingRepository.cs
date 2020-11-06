namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class AdvertisingRepository : GenericRepository<Advertising>, IAdvertisingRepository
    {
        private readonly DataContext _context;


        public AdvertisingRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<Advertising>> GetAdvertisingSatus1Async()
        {
            var advertising = await _context.Advertisings
                 .Include(a => a.Partner)
                 .Where(st => st.Status == 1)
                 .ToListAsync();

            return advertising;

        }

        public async Task<Advertising> GetByIdWithIncludesAsync(int id)
        {
            var advertising = await _context.Advertisings
                 .Include(a => a.CreatedBy)
                 .Include(a => a.Partner)
                 .Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();

            return advertising;
        }


        public async Task<Response> CreatePostAsync(Advertising item)
        {
            try
            {
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
                    Success = false,
                    Message = "Your post was not good enough for the Newspaper. Please try again.\n If the problem persists contact an Administrator."
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    Success = false,
                    Message = "An error ocurred while saving your post. Please try again.\n If the problem persists contact an Administrator."
                };
            }
        }


        public async Task<Response> UpdatePostAsync(Advertising item)
        {
            try
            {
                var result = await UpdateAsync(item);

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
                    Message = "Your changes to this post were not good enough for the Newspaper. Please try again.\n If the problem persists contact an Administrator."
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    Success = false,
                    Message = "An error ocurred while saving your changes. Please try again.\n If the problem persists contact an Administrator."
                };
            }
        }
    }
}