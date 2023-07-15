using back.Data;
using back.Models;
using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace back.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HospitalContext _context;

        public NotificationRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> DeleteNotificationAsync(Notification notification)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification?> GetNotificationByIdAsync(long id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification> UpdateNotificationAsync(Notification notification)
        {
            _context.Entry(notification).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
