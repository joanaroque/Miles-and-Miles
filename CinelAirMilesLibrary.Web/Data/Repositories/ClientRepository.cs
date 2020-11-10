namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Enums;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;


    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(
            DataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return _context.Users
                .Include(c => c.Country)
                .Where(u => u.IsActive == true);
        }

        public IEnumerable<User> GetInactiveUsers()
        {
            return _context.Users
                .Include(c => c.Country)
                .Where(u => u.IsActive == false);
        }

        public IEnumerable<User> GetNewClients()
        {
            return _context.Users
                .Include(c => c.Country)
                .Where(u => u.IsApproved == false);
        }

        public IEnumerable<SelectListItem> GetComboStatus()
        {
            var list = Enum.GetValues(typeof(TierType)).Cast<TierType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();


            return list;
        }

        public IEnumerable<SelectListItem> GetComboGenders()
        {
            var list = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();


            return list;
        }


        public string CreateGuid()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToUInt32(buffer).ToString().Substring(0, 9);
        }


        public async Task<User> GetCurrentClient(int clientId)
        {
            var client = await _context.Users
              .Where(c => c.Id == clientId.ToString()).FirstOrDefaultAsync();

            return client;
        }
    }
}