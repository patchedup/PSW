using back.Models;

namespace back.Services
{
    public interface IBlogsService
    {
        Task<List<Blog>> GetBlogsAsync();

        Task<Blog> CreateBlogAsync(Blog blog, long authorId);
    }
}
