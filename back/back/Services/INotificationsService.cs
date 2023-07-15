using back.Models;

namespace back.Services
{
    public interface INotificationsService
    {
        Task<List<Notification>> GetNotificationsAsync();

        Task<Notification> CreateNotificationAsync(Notification notification, long authorId);
    }
}
