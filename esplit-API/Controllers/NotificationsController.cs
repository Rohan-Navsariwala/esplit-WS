using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		public NotificationsController() { }

		public List<Notification> GetNotifications()
		{
			return new List<Notification>();
		}

		public IActionResult DeleteNotification()
		{
			return null;
		}

		public IActionResult CreateNotification()
		{
			return null;
		}
	}
}
