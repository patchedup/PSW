using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using back.Models;
using back.Repositories;
using back.Services;
using Moq;
using Xunit;

namespace back.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> _userRepositoryMock;
        private readonly UsersService _userService;

        public UsersServiceTests()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _userService = new UsersService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<User> { new User(), new User() };
            _userRepositoryMock.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetUsersAsync();

            // Assert
            Assert.Equal(users, result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ExistingId_ReturnsUser()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(id)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(id);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserByIdAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            var id = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(id)).ReturnsAsync((User)null);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.GetUserByIdAsync(id));
        }

        [Fact]
        public async Task CreateUserAsync_CallsRepositoryMethod()
        {
            // Arrange
            var user = new User();

            // Act
            await _userService.CreateUserAsync(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_ExistingId_CallsRepositoryMethod()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(id)).ReturnsAsync(user);

            // Act
            await _userService.UpdateUserAsync(id, user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.UpdateUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(id)).ReturnsAsync((User)null);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.UpdateUserAsync(id, user));
        }

        [Fact]
        public async Task DeleteUserAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var user = new User { Id = id };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(id)).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteUserAsync(id);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(id)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.DeleteUserAsync(id);

            // Assert
            Assert.False(result);
            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
