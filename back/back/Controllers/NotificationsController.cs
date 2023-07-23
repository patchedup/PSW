using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back.Data;
using back.Models;
using back.Services;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            var notifications = await _notificationsService.GetNotificationsAsync();
            if (notifications == null)
            {
                return NotFound();
            }
            return notifications;
        }

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null)
            {
                return Problem("Entity set userIdString  is null.");
            }
            long userId = long.Parse(userIdString);

            var newNotification= await _notificationsService.CreateNotificationAsync(notification, userId);

            return CreatedAtAction(nameof(PostNotification), new { id = newNotification.Id }, newNotification);
        }
    }
}
