using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly NotificationService _notificationService;
		public NotificationsController() 
		{
			_notificationService = new NotificationService();
		}

		[HttpGet]
		public IActionResult GetNotifications(int userID)
		{
			//auth checks
			List<Notification> notifications = _notificationService.GetNotifications(userID);
			if (notifications != null && notifications.Count > 0)
			{
				return Ok(notifications);
			}
			else
			{
				return NotFound("No notifications found for the given user.");
			}
		}

		[HttpDelete("{userID}/{notificationId}")]
		public IActionResult DeleteNotification(int userID,int notificationId)
		{
			if(_notificationService.DeleteNotification(notificationId, userID))
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
