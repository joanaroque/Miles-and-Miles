
namespace MilesBackOffice.Web.Data.Repositories
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;


    public interface IClientRepository
    {
        IEnumerable<SelectListItem> GetComboStatus();


        IEnumerable<SelectListItem> GetComboGenders();


        IQueryable GetNewClients();


        IQueryable GetActiveUsers();


        IQueryable GetInactiveUsers();

    }
}
