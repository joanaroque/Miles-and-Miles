using Microsoft.AspNetCore.Identity;
using MilesTests.Data.Entities;
using System.Threading.Tasks;

namespace MilesTests.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly UserManager<User> _userManager;

        public SeedDB(DataContext context,
            IUserHelper userHelper,
             UserManager<User> userManager)
        {
            _context = context;
            _userHelper = userHelper;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
