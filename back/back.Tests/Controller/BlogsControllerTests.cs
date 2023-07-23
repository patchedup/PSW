using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using back.Controllers;
using back.Models;
using back.Services;

namespace back.Tests.Controllers
{
    public class BlogsControllerTests
    {
        private readonly Mock<IBlogsService> _blogsServiceMock;
        private readonly BlogsController _blogsController;

        public BlogsControllerTests()
        {
            _blogsServiceMock = new Mock<IBlogsService>();
            _blogsController = new BlogsController(_blogsServiceMock.Object);
        }

        [Fact]
        public async Task GetBlogs_NoBlogsExist_ReturnsNotFoundResult()
        {
            // Arrange
            _blogsServiceMock.Setup(service => service.GetBlogsAsync()).ReturnsAsync((List<Blog>)null);

            // Act
            var result = await _blogsController.GetBlogs();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostBlog_ValidBlog_ReturnsCreatedAtActionResultWithNewBlog()
        {
            // Arrange
            var blog = new Blog();
            var authorId = 1L;
            var newBlog = new Blog { Id = 1 };

            _blogsServiceMock.Setup(service => service.CreateBlogAsync(blog, authorId)).ReturnsAsync(newBlog);
            _blogsController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, authorId.ToString())
                    }, "TestAuthentication"))
                }
            };

            // Act
            var result = await _blogsController.PostBlog(blog);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedBlog = Assert.IsAssignableFrom<Blog>(createdAtActionResult.Value);
            Assert.Equal(newBlog, returnedBlog);
            Assert.Equal("PostBlog", createdAtActionResult.ActionName);
        }
    }
}
