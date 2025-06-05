using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;
using Microsoft.AspNetCore.Authorization;
using Common.Utils;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly NotificationService _notificationService;
		public NotificationsController(Identity common) 
		{
			_notificationService = new NotificationService(common);
		}

		[Authorize]
		[HttpGet]
		public IActionResult GetNotifications()
		{
			//auth checks
			List<Notification> notifications = _notificationService.GetNotifications();
			if (notifications != null && notifications.Count > 0)
			{
				return Ok(notifications);
			}
			else
			{
				return NotFound("No notifications found");
			}
		}

		[Authorize]
		[HttpDelete("{notificationId}")]
		public IActionResult DeleteNotification(int notificationId)
		{
			if(_notificationService.DeleteNotification(notificationId))
			{
				return Ok("Notification deleted successfully.");
			}
			else
			{
				return NotFound("Notification could not be deleted.");
			}
		}

		[HttpPost]
		public IActionResult CreateNotification(Notification notification)
		{
			int notificationId = _notificationService.CreateNotification(notification);
			if(notificationId > 0)
			{
				return Ok(notificationId);
			}
			else
			{
				return BadRequest("Failed to create notification.");
			}
		}
	}
}
