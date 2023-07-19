using back.Models;
using back.Repositories;
using System.Reflection.Metadata;

namespace back.Services
{
    public class NotificationsService : INotificationsService
    {
        INotificationRepository _repository;
        IUsersService _usersService;
        public NotificationsService(INotificationRepository repository, IUsersService userService)
        {
            _usersService = userService;
            _repository = repository;
        }
        public async Task<Notification> CreateNotificationAsync(Notification notification, long authorId)
        {
            var user = await _usersService.GetUserByIdAsync(authorId);
            notification.Admin = user;
            notification.AdminId = authorId;

            var newNotification = await _repository.CreateNotificationAsync(notification);
            return newNotification;
        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            return await _repository.GetNotificationsAsync();
        }
    }
}
