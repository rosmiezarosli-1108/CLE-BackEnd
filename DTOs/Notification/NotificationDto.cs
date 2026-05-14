namespace CLE_BackEnd.DTOs.Notification;

public class NotificationDto
{
    public int Id { get; set; }
    public string ReceiverId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? ROTNumber { get; set; }
    public int? ContianerId { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}
