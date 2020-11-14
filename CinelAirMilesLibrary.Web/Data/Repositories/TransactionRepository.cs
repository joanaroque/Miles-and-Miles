namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public TransactionRepository(
            DataContext context,
            IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IEnumerable<Transaction>> GetAllByClient(string id)
        {
            var list = await _context.Transactions
                .Where(u => u.User.Id == id)
                .ToListAsync();

            return list;
        }


        public int GetStatusMiles(User user)
        {
            var miles = _context.Users
                .Include(u => u.StatusMiles)
                .FirstOrDefault(u => u.Id == user.Id);

            return miles.StatusMiles;
        }

        public int GetBonusMiles(User user)
        {
            var miles = _context.Users
                .Include(u => u.BonusMiles)
                .FirstOrDefault(u => u.Id == user.Id);

            return miles.BonusMiles;
        }

        public async Task<IEnumerable<Transaction>> GetByClientIdAsync(string id)
        {
            return await _context.Transactions
                .Include(u => u.CreatedBy)
                .Include(po => po.Product)
                .Where(i => i.CreatedBy.Id.Equals(id))
                .ToListAsync();
        }

        public async Task<Response> AddTransanctionAsync(Transaction trans)
        {
            var result = await CreateAsync(trans);

            if (!result)
            {
                return new Response
                {
                    Message = "An error ocurred while submitting your request. Please try again.",
                    Success = false
                };
            }
            return new Response
            {
                Success = true
            };
        }


        public bool GetTransactionHistory(User user)
        {
            var year = DateTime.Now.Year - user.AccountApprovedDate.Year;
            var season = user.AccountApprovedDate.AddYears(year);
            var trans = _context.Transactions
                .Include(u => u.CreatedBy)
                .Where(f => f.CreatedBy.Id == user.Id && f.CreateDate > season);
            int total = 0;
            foreach (var item in trans)
            {
                total += item.Value;
            }

            if (total > 20000)
            {
                return false;
            }
            return true;
        }
    }
}
