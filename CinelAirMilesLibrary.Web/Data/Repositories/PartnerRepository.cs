namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using CinelAirMilesLibrary.Common.Helpers;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Partner>> GetAllIncludes()
        {
            var partner = await _context.Partners
               // .Include(u => u.CreatedBy)
               .ToListAsync();

            return partner;
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
                list = _context.Partners.Where(p => p.Status == 0).Select(item => new SelectListItem
                {
                    Text = item.CompanyName,
                    Value = item.Id.ToString()
                })
                .ToList();
            });
            return list;
        }


        public async Task<IEnumerable<SelectListItem>> GetComboAirlines()
        {
            var list = new List<SelectListItem>();
            await Task.Run(() =>
            {
                list = _context.Partners.Where(p => p.Status == 0 && p.Designation == PartnerType.Airline)
                .Select(item => new SelectListItem
                {
                    Text = item.CompanyName,
                    Value = item.Id.ToString()
                })
                .ToList();
            });
            return list;
        }


        public IEnumerable<SelectListItem> GetComboPartnerTypes()
        {
            var list = Enum.GetValues(typeof(PartnerType)).Cast<PartnerType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            return list;
        }


        public async Task<List<Partner>> GetPartnerWithStatus1Async()
        {
            var partner = await _context.Partners
                 .Where(st => st.Status == 1)
                 .ToListAsync();

            return partner;
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
