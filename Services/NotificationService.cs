using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Notification;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class NotificationService : INotificationService {
    private readonly ApplicationDbContext _dbContext;
    public NotificationService(ApplicationDbContext context) => _dbContext = context;

    public async Task CreateNotification(string userId, string message, string rotNumber, int containerId) {
        var notification = new Notification {
            ReceiverId = userId,
            Message = message,
            ROTNumber = rotNumber,
            ContainerId = containerId,
            CreatedAt = DateTime.UtcNow
        };
        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadByUserId(string userId) {
        return await _dbContext.Notifications
            .Where(n => n.ReceiverId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationDto {
                Id = n.Id,
                Message = n.Message,
                ROTNumber = n.ROTNumber,
                ContainerId = n.ContainerId,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToListAsync();
    }

    public async Task MarkAsRead(int id) {
        var notif = await _dbContext.Notifications.FindAsync(id);
        if (notif != null) {
            notif.IsRead = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}