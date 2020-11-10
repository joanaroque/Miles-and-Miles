using CinelAirMilesLibrary.Common.Data.Entities;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<SelectListItem> GetComboStatus();


        IEnumerable<SelectListItem> GetComboGenders();


        IEnumerable<User> GetNewClients();


        IEnumerable<User> GetActiveUsers();


        IEnumerable<User> GetInactiveUsers();


        string CreateGuid();


        /// <summary>
        /// get de current client
        /// </summary>
        /// <returns>current client</returns>
        Task<User> GetCurrentClient(int clientId);
    }
}
