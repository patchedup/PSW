using back.Models;
using back.Repositories;

namespace back.Services
{
    public class NotificationService : INotificationsService
    {
        private readonly INotificationRepository _notificationsRepository;

        public NotificationService(INotificationRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;

        }

        public Task<Notification> CreateNotificationAsync(Notification notification, long authorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetNotificationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
