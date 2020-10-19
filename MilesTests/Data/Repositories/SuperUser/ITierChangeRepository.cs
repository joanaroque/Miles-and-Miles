namespace MilesBackOffice.Web.Data.Repositories.SuperUser
{

    using MilesBackOffice.Web.Data.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface ITierChangeRepository : IGenericRepository<TierChange>
    {
        Task<List<TierChange>> GetAllClientListAsync();


        Task<TierChange> GetByIdWithIncludesAsync(int id);
    }
}
