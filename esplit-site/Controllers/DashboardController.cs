using Microsoft.AspNetCore.Mvc;

namespace esplit_site.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		//return all the splits for the given type
		public IActionResult Splits()
		{
			return View();
		}

		//this is gonna return the dialogue box of participants of specific split
		public IActionResult SplitParticipants()
		{
			return View();
		}

		public IActionResult AddParticipant()
		{
			return View();
		}

		public IActionResult Approve()
		{
			return Ok();
		}
		
		public IActionResult Reject()
		{
			return Ok();
		}
		
		public IActionResult Pay()
		{
			return Ok();
		}

		public IActionResult Close()
		{
			return Ok();
		}
	}
}
