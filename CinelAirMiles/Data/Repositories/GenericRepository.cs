namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;


    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContextClients _context;

        public GenericRepository(DataContextClients context)
        {
            _context = context;
        }


        public async Task<bool> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await SaveAllAsync();
        }


        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await SaveAllAsync();
        }


        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }



        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await SaveAllAsync();
        }
    }
}
