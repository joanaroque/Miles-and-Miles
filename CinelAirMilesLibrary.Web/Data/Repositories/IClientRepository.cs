using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<SelectListItem> GetComboStatus();


        IEnumerable<SelectListItem> GetComboGenders();


        IQueryable GetNewClients();


        IQueryable GetActiveUsers();


        IQueryable GetInactiveUsers();


        string CreateGuid();
    }
}
