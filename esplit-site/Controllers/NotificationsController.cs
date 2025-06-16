using Biz.Services;
using Common.Types;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace esplit_site.Controllers
{
	[Route("{controller}")]
	public class NotificationsController : BaseController
	{
		private NotificationService notifyService;
		public NotificationsController(Identity common) : base(common)
		{
			notifyService = new NotificationService(common);
			ViewData["username"] = common.GetClaims().username;
		}
		[Authorize]
		public IActionResult Index()
		{
			List<Notification> notifications = notifyService.GetNotifications();
			return View(notifications);
		}

		//what we can do is redirect to index in order to get the notifications refreshed, or i can just get the response code and perform removing from list action through JS, by that i will practice js also

		[Authorize]
		[HttpDelete]
		public IActionResult Delete(int NotificationID)
		{
			if (notifyService.DeleteNotification(NotificationID))
			{
				return Ok(new { success = true, message = "Notification deleted successfully." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to delete notification." });
			}
		}
	}
}
