using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable GetActiveClients()
        {
            //todo ver se é cliente
            return _context.Users
                .Where(u => u.IsActive == true);
        }

        public IQueryable GetInactiveClients()
        {
            //todo ver se é cliente
            return _context.Users
                .Where(u => u.IsActive == false);
        }

        public IQueryable GetNewClients()
        {
            return _context.Users
                .Where(u => u.IsApproved == false);
        }
    }
}
