namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class TierChangeRepository : GenericRepository<TierChange>, ITierChangeRepository
    {
        private readonly DataContext _context;

        public TierChangeRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<TierChange> GetByIdWithIncludesAsync(int id)
        {
            var tierChange = await _context.TierChanges
                 .Include(t => t.Client)
                 .Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

            return tierChange;
        }

        public async Task<List<TierChange>> GetAllClientListAsync()
        {
            var tierChange = await _context.TierChanges
                 .Include(t => t.Client).ToListAsync();

            return tierChange;
        }

        //public async Task<TierChange> ChangeTierClient(User client)
        //{
        //    var tier = await _context.TierChanges.FirstOrDefaultAsync();

        //    var miles = await _context.Transactions
        //        .Where(u => u.User == client
        //        && u.CreateDate > DateTime.Now.AddYears(-1)
        //        && u.CreateDate < DateTime.Today)
        //        .FirstOrDefaultAsync();

        //    //todo por no cliente para SILVER: se o cliente acumular num ano 30.000 milhas ou ter voado 25 segmentos
        //    // nas transtactions TransactionType credito e type == status
        //    //if (client.StatusMiles >= 30000 || )
        //    //{

        //    //}
        //    ////para GOLD 70.000 milhas ou 50 segmentos

        //    return tier;
            
        //}

        //public async Task<TierChange> UpgradeCancelation()
        //{
        //    var tier = _context.TierChanges.FirstOrDefault();

        //    if (tier.OldTier != TierType.Gold)
        //    {
        //        // todo mesage that says he must pay 80€ of tee
        //    }

        //    await UpdateAsync(tier);


        //    return tier;
        //}
    }
}
