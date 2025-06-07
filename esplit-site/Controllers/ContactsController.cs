using Microsoft.AspNetCore.Mvc;

namespace esplit_site.Controllers
{
	public class ContactsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SendRequest()
		{
			return Ok();
		}

		public IActionResult Approve()
		{
			return Ok();
		}
		public IActionResult Reject()
		{
			return Ok();
		}

		public IActionResult Delete()
		{
			return Ok();
		}
	}
}
