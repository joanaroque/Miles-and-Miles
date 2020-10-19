namespace MilesBackOffice.Web.Data.Repositories.User
{
    using System;
    using System.Threading.Tasks;

    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Helpers;

    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private readonly DataContext _context;

        public NewsRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }


        public async Task<Response> CreatePostAsync(News item)
        {
            try
            {
                var result = await CreateAsync(item);

                if (result)
                {
                    return new Response
                    {
                        Success = true
                    };
                }

                return new Response
                {
                    Success = false,
                    Message = "Your post was not good enough for the Newspaper. Please try again.\n If the problem persists contact an Administrator."
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    Success = false,
                    Message = "An error ocurred while saving your post. Please try again.\n If the problem persists contact an Administrator."
                };
            }
        }


        public async Task<Response> UpdatePostAsync(News item)
        {
            try
            {
                var result = await UpdateAsync(item);

                if (result)
                {
                    return new Response
                    {
                        Success = true
                    };
                }

                return new Response
                {
                    Success = false,
                    Message = "Your changes to this post were not good enough for the Newspaper. Please try again.\n If the problem persists contact an Administrator."
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    Success = false,
                    Message = "An error ocurred while saving your changes. Please try again.\n If the problem persists contact an Administrator."
                };
            }
        }
    }
}
