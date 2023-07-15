using back.Models;

namespace back.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsAsync();
        Task<Notification?> GetNotificationByIdAsync(long id);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<Notification> UpdateNotificationAsync(Notification notification);
        Task<Notification> DeleteNotificationAsync(Notification notification);
    }
}
