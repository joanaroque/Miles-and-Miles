namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using MilesBackOffice.Web.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClientComplaintRepository : IGenericRepository<ClientComplaint>
    {

        Task<List<ClientComplaint>> GetClientComplaintsAsync();



        Task<ClientComplaint> GetByIdWithIncludesAsync(int id);

    }
}
