using back.Dtos;
using back.Models;
using back.Services;
using Moq;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace back.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IUsersService> _usersServiceMock;

        public AuthServiceTests()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _authService = new AuthService(_usersServiceMock.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsLoggedInUserDTO()
        {
            // Arrange
            var loginDTO = new LoginDTO("user@example.com", "password");
            var user = new User
            {
                Id = 1,
                Email = loginDTO.Email,
                Password = loginDTO.Password,
                Role = "PATIENT"
            };

            _usersServiceMock.Setup(service => service.FindUserByEmailAsync(loginDTO.Email))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.Login(loginDTO);

            // Assert
            Assert.NotNull(result.Token);
            Assert.Equal(user, result.User);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ThrowsException()
        {
            // Arrange
            var loginDTO = new LoginDTO("user@example.com", "wrongpassword");
            var user = new User
            {
                Id = 1,
                Email = loginDTO.Email,
                Password = "password",
                Role = "PATIENT"
            };

            _usersServiceMock.Setup(service => service.FindUserByEmailAsync(loginDTO.Email))
                .ReturnsAsync(user);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.Login(loginDTO));
        }
    }
}
