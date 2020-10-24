namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using MilesBackOffice.Web.Data;
    using MilesBackOffice.Web.Data.Repositories;

    public class ComplaintRepository : GenericRepository<ClientComplaint>, IComplaintRepository
    {
        private readonly DataContext _context;

        public ComplaintRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
