namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        private readonly DataContext _context;

        public PartnerRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }


        public async Task<Response> AddPartnerAsync(Partner model)
        {
            try
            {
                var result = await CreateAsync(model);

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
                    Message = "The Partner was not added. Please try again.\n If the problem persists contact an Administrator."
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


        public async Task<Partner> GetByIdWithIncludesAsync(int id)
        {
            var partner = await _context.Partners
                 .Include(a => a.CreatedBy)
                 .Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();

            return partner;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboPartners()
        {
            var list = new List<SelectListItem>();
            await Task.Run(() =>
            {
                list = _context.Partners.Select(item => new SelectListItem
                {
                    Text = item.CompanyName,
                    Value = item.Id.ToString()
                }).ToList();
            });
            return list;
        }


        public async Task<Response> UpdatePartnerAsync(Partner model)
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
                    Message = "Could not update the entry. Please try again.\n  If the problem persists contact an Administrator."
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
