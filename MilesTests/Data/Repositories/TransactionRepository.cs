namespace MilesBackOffice.Web.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
