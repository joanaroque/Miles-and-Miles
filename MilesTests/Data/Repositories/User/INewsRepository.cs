namespace MilesBackOffice.Web.Data.Repositories.User
{
    using System.Threading.Tasks;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Helpers;

    public interface INewsRepository : IGenericRepository<News>
    {
        Task<Response> CreatePostAsync(News item);



        Task<Response> UpdatePostAsync(News item);
    }
}
