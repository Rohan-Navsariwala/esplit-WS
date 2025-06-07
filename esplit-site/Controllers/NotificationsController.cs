using Microsoft.AspNetCore.Mvc;

namespace esplit_site.Controllers
{
	public class NotificationsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		//what we can do is redirect to index in order to get the notifications refreshed, or i can just get the response code and perform removing from list action through JS, by that i will practice js also
		public IActionResult Delete()
		{
			return Ok();
		}
	}
}
