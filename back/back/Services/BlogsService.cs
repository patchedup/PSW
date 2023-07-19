using back.Models;
using back.Repositories;

namespace back.Services
{
    public class BlogsService : IBlogsService
    {
        IBlogRepository _repository;
        IUsersService _usersService;
        public BlogsService(IBlogRepository repository, IUsersService userService) {
            _usersService = userService;
            _repository = repository;
        }
        public async Task<Blog> CreateBlogAsync(Blog blog, long authorId)
        {
            var user = await _usersService.GetUserByIdAsync(authorId);
            blog.User = user;
            blog.UserId = authorId;

            var newBlog = await _repository.CreateBlogAsync(blog);
            return newBlog;

        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _repository.GetBlogsAsync();
        }
    }
}
