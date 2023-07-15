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
        public async Task BlockUser_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var id = 1;
            var blockedUser = new User { Id = id, IsBlocked = 1 };
            _userServiceMock.Setup(service => service.BlockUserAsync(id)).ReturnsAsync(blockedUser);

            // Act
            var result = await _usersController.BlockUser(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(blockedUser, returnedUser);
        }

        [Fact]
        public async Task UnblockUser_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var id = 1;
            var unblockedUser = new User { Id = id, IsBlocked = 0 };
            _userServiceMock.Setup(service => service.UnblockUserAsync(id)).ReturnsAsync(unblockedUser);

            // Act
            var result = await _usersController.UnblockUser(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(unblockedUser, returnedUser);
        }
    }
}
