namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using MilesBackOffice.Web.Data;

    public class ComplaintRepository : GenericRepository<ClientComplaint>, IComplaintRepository
    {
        private readonly DataContextClients _context;

        public ComplaintRepository(DataContextClients context) : base(context)
        {
            _context = context;
        }
    }
}
