namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClientComplaintRepository : IGenericRepository<ClientComplaint>
    {

        Task<List<ClientComplaint>> GetClientComplaintsAsync();



        Task<ClientComplaint> GetByIdWithIncludesAsync(int id);

    }
}
