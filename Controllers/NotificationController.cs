using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase {
    private readonly INotificationService _notifService;
    public NotificationController(INotificationService s) => _notifService = s;

    [HttpGet("unread/{userId}")]
    public async Task<IActionResult> GetUnread(string userId) => 
        Ok(await _notifService.GetUnreadByUserId(userId));

    [HttpPost("read/{id}")]
    public async Task<IActionResult> MarkRead(int id) {
        await _notifService.MarkAsRead(id);
        return Ok();
    }
}