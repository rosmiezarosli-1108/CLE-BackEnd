using CLE_BackEnd.DTOs.Notification;

namespace CLE_BackEnd.Services;

public interface INotificationService {
    Task CreateNotification(string userId, string message, string rotNumber, int containerId);
    Task<IEnumerable<NotificationDto>> GetUnreadByUserId(string userId);
    Task MarkAsRead(int notificationId);
}