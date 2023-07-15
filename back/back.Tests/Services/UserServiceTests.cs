using back.Models;
using back.Repositories;
using back.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace back.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly UsersService _usersService;
        private readonly Mock<IUsersRepository> _userRepositoryMock;

        public UsersServiceTests()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _usersService = new UsersService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ValidRequest_ReturnsUsers()
        {
            // Arrange
            var expectedUsers = new List<User> { new User { Id = 1 }, new User { Id = 2 } };
            _userRepositoryMock.Setup(repository => repository.GetUsersAsync()).ReturnsAsync(expectedUsers);

            // Act
            var users = await _usersService.GetUsersAsync();

            // Assert
            Assert.Equal(expectedUsers, users);
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidId_ReturnsUser()
        {
            // Arrange
            var id = 1L;
            var expectedUser = new User { Id = id };
            _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(id)).ReturnsAsync(expectedUser);

            // Act
            var user = await _usersService.GetUserByIdAsync(id);

            // Assert
            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public async Task GetUserByIdAsync_InvalidId_ThrowsException()
        {
            // Arrange
            var id = 1L;
            _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(id)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _usersService.GetUserByIdAsync(id));
        }

        [Fact]
        public async Task FindUserByEmailAsync_ValidEmail_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";
            var expectedUser = new User { Email = email };
            _userRepositoryMock.Setup(repository => repository.FindByEmailAsync(email)).ReturnsAsync(expectedUser);

            // Act
            var user = await _usersService.FindUserByEmailAsync(email);

            // Assert
            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public async Task FindUserByEmailAsync_InvalidEmail_ThrowsException()
        {
            // Arrange
            var email = "test@example.com";
            _userRepositoryMock.Setup(repository => repository.FindByEmailAsync(email)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _usersService.FindUserByEmailAsync(email));
        }

    }
}
