using CinelAirMilesLibrary.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinelAirMiles.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContextClients _context;

        public ClientRepository(
            DataContextClients context)
        {
            _context = context;
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
    }
}