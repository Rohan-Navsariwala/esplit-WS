using Biz.Services;
using Common.Types;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace esplit_site.Controllers
{
	public class NotificationsController : Controller
	{
		private NotificationService notifyService;
		public NotificationsController(Identity common)
		{
			notifyService = new NotificationService(common);
		}
		[Authorize]
		public IActionResult Index()
		{
			List<Notification> notifications = notifyService.GetNotifications();
			return View(notifications);
		}

		//what we can do is redirect to index in order to get the notifications refreshed, or i can just get the response code and perform removing from list action through JS, by that i will practice js also
		public IActionResult Delete()
		{
			return Ok();
		}
	}
}
