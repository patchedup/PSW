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
using back.Dtos;

namespace back.Tests.Controllers
{
    public class InternistDatasControllerTests
    {
        private readonly Mock<IInternistDataService> _internistDataServiceMock;
        private readonly InternistDatasController _internistDatasController;

        public InternistDatasControllerTests()
        {
            _internistDataServiceMock = new Mock<IInternistDataService>();
            _internistDatasController = new InternistDatasController(_internistDataServiceMock.Object);
        }

        [Fact]
        public async Task GetInternistDataForUser_ValidUser_ReturnsOkResultWithInternistData()
        {
            // Arrange
            var userId = 1;
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            _internistDatasController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var internistData = new List<InternistData> { new InternistData(), new InternistData() };
            _internistDataServiceMock.Setup(service => service.GetInternistDatasForUserAsync(userId)).ReturnsAsync(internistData);

            // Act
            var result = await _internistDatasController.GetInternistDataForUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedInternistData = Assert.IsAssignableFrom<IEnumerable<InternistData>>(okResult.Value);
            Assert.Equal(internistData, returnedInternistData);
        }

        [Fact]
        public async Task PostInternistData_ValidInternistData_ReturnsCreatedAtActionResultWithInternistData()
        {
            // Arrange
            var userId = 1;
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            _internistDatasController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var internistDataDto = new InternistDataDTO
            {
                BloodPressure = "120/80",
                BloodSugar = 100,
                BodyFat = 25,
                Weight = 70,
                MenstruationStartDate = "2023-01-01",
                MenstruationEndDate = "2023-01-07"
            };

            var createdInternistData = new InternistData
            {
                Id = 1,
                BloodPressure = "120/80",
                BloodSugar = 100,
                BodyFat = 25,
                Weight = 70,
                Menstruation_start_date = "2023-01-01",
                Menstruation_end_date = "2023-01-07"
            };

            _internistDataServiceMock.Setup(service => service.CreateInternistDataAsync(internistDataDto, userId)).ReturnsAsync(createdInternistData);

            // Act
            var result = await _internistDatasController.PostInternistData(internistDataDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedInternistData = Assert.IsAssignableFrom<InternistData>(createdAtActionResult.Value);
            Assert.Equal(createdInternistData, returnedInternistData);
            Assert.Equal("GetInternistData", createdAtActionResult.ActionName);
        }
    }
}
