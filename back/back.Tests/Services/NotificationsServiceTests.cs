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
    public class NotificationsServiceTests
    {
        private readonly Mock<INotificationRepository> _notificationRepositoryMock;
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly INotificationsService _notificationsService;

        public NotificationsServiceTests()
        {
            _notificationRepositoryMock = new Mock<INotificationRepository>();
            _usersServiceMock = new Mock<IUsersService>();
            _notificationsService = new NotificationsService(_notificationRepositoryMock.Object, _usersServiceMock.Object);
        }

        [Fact]
        public async Task CreateNotificationAsync_ReturnsNewNotification()
        {
            // Arrange
            var authorId = 1;
            var notification = new Notification();
            var user = new User { Id = authorId };
            var createdNotification = new Notification { Id = 1 };

            _usersServiceMock.Setup(service => service.GetUserByIdAsync(authorId)).ReturnsAsync(user);
            _notificationRepositoryMock.Setup(repo => repo.CreateNotificationAsync(notification)).ReturnsAsync(createdNotification);

            // Act
            var result = await _notificationsService.CreateNotificationAsync(notification, authorId);

            // Assert
            Assert.Equal(createdNotification, result);
            Assert.Equal(user, notification.Admin);
            Assert.Equal(authorId, notification.AdminId);
        }

        [Fact]
        public async Task GetNotificationsAsync_ReturnsListOfNotifications()
        {
            // Arrange
            var notifications = new List<Notification> { new Notification(), new Notification() };
            _notificationRepositoryMock.Setup(repo => repo.GetNotificationsAsync()).ReturnsAsync(notifications);

            // Act
            var result = await _notificationsService.GetNotificationsAsync();

            // Assert
            Assert.Equal(notifications, result);
        }
    }
}
