using back.Models;
using back.Repositories;
using back.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace back.Tests.Services
{
    public class BlogsServiceTests
    {
        private readonly BlogsService _blogsService;
        private readonly Mock<IBlogRepository> _blogRepositoryMock;
        private readonly Mock<IUsersService> _usersServiceMock;

        public BlogsServiceTests()
        {
            _blogRepositoryMock = new Mock<IBlogRepository>();
            _usersServiceMock = new Mock<IUsersService>();
            _blogsService = new BlogsService(_blogRepositoryMock.Object, _usersServiceMock.Object);
        }

        [Fact]
        public async Task CreateBlogAsync_ValidData_ReturnsNewBlog()
        {
            // Arrange
            long authorId = 1;
            var blog = new Blog
            {
                Title = "Sample Blog",
                Content = "This is a sample blog content."
            };

            var user = new User { Id = authorId };
            _usersServiceMock.Setup(service => service.GetUserByIdAsync(authorId))
                .ReturnsAsync(user);

            var newBlog = new Blog
            {
                Id = 1,
                Title = blog.Title,
                Content = blog.Content,
                UserId = authorId,
                User = user
            };

            _blogRepositoryMock.Setup(repo => repo.CreateBlogAsync(It.IsAny<Blog>()))
                .ReturnsAsync(newBlog);

            // Act
            var result = await _blogsService.CreateBlogAsync(blog, authorId);

            // Assert
            Assert.Equal(newBlog.Id, result.Id);
            Assert.Equal(newBlog.Title, result.Title);
            Assert.Equal(newBlog.Content, result.Content);
            Assert.Equal(newBlog.UserId, result.UserId);
            Assert.Equal(newBlog.User, result.User);
        }

        [Fact]
        public async Task GetBlogsAsync_ReturnsListOfBlogs()
        {
            // Arrange
            var blogs = new List<Blog>
            {
                new Blog { Id = 1, Title = "Blog 1", Content = "Content 1" },
                new Blog { Id = 2, Title = "Blog 2", Content = "Content 2" }
            };

            _blogRepositoryMock.Setup(repo => repo.GetBlogsAsync())
                .ReturnsAsync(blogs);

            // Act
            var result = await _blogsService.GetBlogsAsync();

            // Assert
            Assert.Equal(blogs.Count, result.Count);
            Assert.Equal(blogs[0].Id, result[0].Id);
            Assert.Equal(blogs[0].Title, result[0].Title);
            Assert.Equal(blogs[0].Content, result[0].Content);
            Assert.Equal(blogs[1].Id, result[1].Id);
            Assert.Equal(blogs[1].Title, result[1].Title);
            Assert.Equal(blogs[1].Content, result[1].Content);
        }
    }
}
