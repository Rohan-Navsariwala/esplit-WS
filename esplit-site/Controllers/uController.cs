using System.Diagnostics;
using esplit_site.Models;
using Microsoft.AspNetCore.Mvc;

namespace esplit_site.Controllers
{
	public class uController : Controller
	{
		private readonly ILogger<uController> _logger;

		public uController(ILogger<uController> logger)
		{
			_logger = logger;
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Signup()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
