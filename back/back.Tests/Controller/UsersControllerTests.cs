using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back.Controllers;
using back.Models;
using back.Services;
using Moq;
using Xunit;

namespace back.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersService> _userServiceMock;
        private readonly UsersController _usersController;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUsersService>();
            _usersController = new UsersController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResultWithUsers()
        {
            // Arrange
            var users = new List<User> { new User(), new User() };
            _userServiceMock.Setup(service => service.GetUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _usersController.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(users, returnedUsers);
        }

        [Fact]
        public async Task GetUser_ExistingId_ReturnsOkResultWithUser()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };
            _userServiceMock.Setup(service => service.GetUserByIdAsync(id)).ReturnsAsync(user);

            // Act
            var result = await _usersController.GetUser(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(user, returnedUser);
        }

        [Fact]
        public async Task GetUser_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var id = 1;
            _userServiceMock.Setup(service => service.GetUserByIdAsync(id)).ReturnsAsync((User)null);

            // Act
            var result = await _usersController.GetUser(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutUser_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };

            // Act
            var result = await _usersController.PutUser(id, user);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _userServiceMock.Verify(service => service.UpdateUserAsync(id, user), Times.Once);
        }

        [Fact]
        public async Task PutUser_NonExistingId_ReturnsBadRequestResult()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };
            _userServiceMock.Setup(service => service.UpdateUserAsync(id, user)).ThrowsAsync(new Exception());

            // Act
            var result = await _usersController.PutUser(id, user);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PostUser_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var user = new User();
            _userServiceMock.Setup(service => service.CreateUserAsync(user)).ReturnsAsync(user);
            // Act
            var result = await _usersController.PostUser(user);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdUser = Assert.IsAssignableFrom<User>(createdAtActionResult.Value);
            Assert.Equal("GetUser", createdAtActionResult.ActionName);
            Assert.Equal(user.Id, createdUser.Id);
        }

        [Fact]
        public async Task DeleteUser_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            _userServiceMock.Setup(service => service.DeleteUserAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _usersController.DeleteUser(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var id = 1;
            _userServiceMock.Setup(service => service.DeleteUserAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _usersController.DeleteUser(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
