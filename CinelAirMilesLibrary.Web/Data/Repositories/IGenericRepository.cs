namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        //TODO refactor generic repository
        /// <summary>
        /// Gets all items in DataContext with no tracking
        /// Tracking is used when creating / editing / deleting items from datacontext
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets item by it's ID
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
