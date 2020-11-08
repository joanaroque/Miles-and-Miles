namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;
    using Microsoft.EntityFrameworkCore;
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

        public Task<List<Transaction>> GetAllByClient(User user)
        {
            var list = _context.Transactions
                .Where(u => u.User.Id == user.Id)
                .ToListAsync();

            return list;
        }


        public int GetStatusMiles(User user)
        {
            var miles = _context.Users
                .Include(u => u.StatusMiles)
                .FirstOrDefault(u => u.Id == user.Id);

            return miles.BonusMiles;
        }

        public int GetBonusMiles(User user)
        {
            var miles = _context.Users
                .Include(u => u.BonusMiles)
                .FirstOrDefault(u => u.Id == user.Id);

            return miles.BonusMiles;
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
    }
}
