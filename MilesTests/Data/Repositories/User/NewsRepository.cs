namespace MilesBackOffice.Web.Data.Repositories.User
{
    using System.Linq;
    using MilesBackOffice.Web.Data.Entities;

    public class NewsRepository: GenericRepository<News>, INewsRepository
    {
        private readonly DataContext _context;

        public NewsRepository(DataContext context)
            :base (context)
        {
            _context = context;
        }



    }
}
