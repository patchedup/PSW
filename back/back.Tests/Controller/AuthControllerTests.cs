using System.Threading.Tasks;
using back.Controllers;
using back.Dtos;
using back.Models;
using back.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace back.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsOkResultWithUser()
        {
            // Arrange
            var newUser = new User();
            var registeredUser = new User();
            _authServiceMock.Setup(service => service.Register(newUser)).ReturnsAsync(registeredUser);

            // Act
            var result = await _authController.Register(newUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(registeredUser, returnedUser);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResultWithJwtDTO()
        {
            // Arrange
            var loginDTO = new LoginDTO("test@example.com", "password");
            var token = "test-token";
            var user = new User { Id = 1, Email = "test@example.com", FirstName = "John", LastName = "Doe" };
            var loggedInUserDTO = new LoggedInUserDTO(token, user);
            _authServiceMock.Setup(service => service.Login(loginDTO)).ReturnsAsync(loggedInUserDTO);

            // Act
            var result = await _authController.Login(loginDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

    }
}
