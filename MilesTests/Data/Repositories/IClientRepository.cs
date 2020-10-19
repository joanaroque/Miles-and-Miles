using System.Linq;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IClientRepository
    {

        IQueryable GetNewClients();


        IQueryable GetActiveUsers();


        IQueryable GetInactiveUsers();

    }
}
