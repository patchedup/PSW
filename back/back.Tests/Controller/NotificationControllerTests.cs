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
    public class NotificationsControllerTests
    {
        private readonly Mock<INotificationsService> _notificationsServiceMock;
        private readonly NotificationsController _notificationsController;

        public NotificationsControllerTests()
        {
            _notificationsServiceMock = new Mock<INotificationsService>();
            _notificationsController = new NotificationsController(_notificationsServiceMock.Object);
        }

        [Fact]
        public async Task GetNotifications_NoNotificationsExist_ReturnsNotFoundResult()
        {
            // Arrange
            _notificationsServiceMock.Setup(service => service.GetNotificationsAsync()).ReturnsAsync((List<Notification>)null);

            // Act
            var result = await _notificationsController.GetNotifications();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostNotification_ValidNotification_ReturnsCreatedAtActionResultWithNewNotification()
        {
            // Arrange
            var notification = new Notification();
            var authorId = 1L;
            var newNotification = new Notification { Id = 1 };

            _notificationsServiceMock.Setup(service => service.CreateNotificationAsync(notification, authorId)).ReturnsAsync(newNotification);
            _notificationsController.ControllerContext = new ControllerContext
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
            var result = await _notificationsController.PostNotification(notification);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedNotification = Assert.IsAssignableFrom<Notification>(createdAtActionResult.Value);
            Assert.Equal(newNotification, returnedNotification);
            Assert.Equal("GetNotification", createdAtActionResult.ActionName);
        }
    }
}
