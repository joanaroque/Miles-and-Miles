
namespace MilesBackOffice.Web.Data.Repositories
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;


    public interface IClientRepository
    {
        IEnumerable<SelectListItem> GetComboStatus();


        IQueryable GetNewClients();


        IQueryable GetActiveUsers();


        IQueryable GetInactiveUsers();

    }
}
