using System.Linq;
using System.Threading.Tasks;

namespace MilesBackOffice.Web.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        //TODO refactor generic repository
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets item by it's ID. Doesn't track the item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);


        Task<bool> CreateAsync(T entity);


        Task<bool> UpdateAsync(T entity);


        Task<bool> DeleteAsync(T entity);


        Task<bool> ExistAsync(int id);
    }
}
