namespace CinelAirMilesLibrary.Common.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets all items in DataContext with no tracking
        /// Tracking is used when creating / editing / deleting items from datacontext
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets an entity model of class T, by it's ID in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Task with a model of the entity, if any is found</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new entry of class T and saves to Context
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>A Task with a bollean refering the success/failure of the process</returns>
        Task<bool> CreateAsync(T entity);

        /// <summary>
        /// Updates an entry of class T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>A Task with a bollean refering the success/failure of the process</returns>
        Task<bool> UpdateAsync(T entity);


        /// <summary>
        /// Deletes an entry of class T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>A Task with a bollean refering the success/failure of the process</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Checks for an entry of class T, by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Task with a bollean. If any is found true, otherwise false.</returns>
        Task<bool> ExistAsync(int id);
    }
}
