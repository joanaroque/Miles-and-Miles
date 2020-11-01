﻿using CinelAirMilesLibrary.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(
            DataContext context)
        {
            _context = context;
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
    }
}