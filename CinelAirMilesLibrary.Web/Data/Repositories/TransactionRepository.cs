namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System.Threading.Tasks;

    using CinelAirMilesLibrary.Common.Data;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context) : base(context)
        {
            _context = context;
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
