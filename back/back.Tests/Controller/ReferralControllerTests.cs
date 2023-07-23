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
    public class ReferralsControllerTests
    {
        private readonly Mock<IReferralService> _referralServiceMock;
        private readonly ReferralsController _referralsController;

        public ReferralsControllerTests()
        {
            _referralServiceMock = new Mock<IReferralService>();
            _referralsController = new ReferralsController(_referralServiceMock.Object);
        }

        [Fact]
        public async Task PostReferral_ValidReferral_ReturnsCreatedAtActionResultWithReferral()
        {
            // Arrange
            var userId = 1;
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            _referralsController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var referralDto = new ReferralDTO
            {
                Id = 1,
                IsUsed = 0,
                ForDoctorId = 2,
                AppointmentId = 3
            };

            var createdReferral = new Referral
            {
                Id = 1,
                IsUsed = 0,
                ForDoctorId = 2
            };

            _referralServiceMock.Setup(service => service.CreateReferralAsync(referralDto)).ReturnsAsync(createdReferral);

            // Act
            var result = await _referralsController.PostReferral(referralDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedReferral = Assert.IsAssignableFrom<Referral>(createdAtActionResult.Value);
            Assert.Equal(createdReferral, returnedReferral);
            Assert.Equal("PostReferral", createdAtActionResult.ActionName);
            Assert.Equal(createdReferral.Id, createdAtActionResult.RouteValues["id"]);
        }
    }
}
