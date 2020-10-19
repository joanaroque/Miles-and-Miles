using Microsoft.AspNetCore.Identity;
using MilesBackOffice.Web.Data.Entities;
using MilesBackOffice.Web.Enums;
using MilesBackOffice.Web.Helpers;
using System.Linq;

namespace MilesBackOffice.Web.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public ClientRepository(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IQueryable GetActiveUsers()
        {
            return _context.Users
                .Where(u => u.IsActive == true);
        }

        public IQueryable GetInactiveUsers()
        {
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
