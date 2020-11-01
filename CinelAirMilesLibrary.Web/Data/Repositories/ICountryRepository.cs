using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {

        IEnumerable<SelectListItem> GetComboCountries();

    }
}
