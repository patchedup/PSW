using back.Models;
using back.Repositories;

namespace back.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly IBlogRepository _blogsRepository;

        public BlogsService(IBlogRepository blogsRepository)
        {
            _blogsRepository = blogsRepository;

        }

        public Task<Blog> CreateBlogAsync(Blog blog, long authorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Blog>> GetBlogsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
