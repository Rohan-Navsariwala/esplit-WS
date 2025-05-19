using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SplitsController : ControllerBase
	{
		public SplitsController() { }

		public List<Split> GetUserCreatedSplits()
		{
			return new List<Split>();
		}

		public List<Split> GetPendingSplitRequests()
		{
			return new List<Split>();
		}

		public List<Split> GetUserDueSplits()
		{
			return new List<Split>();
		}

		public List<Contact> GetSplitParticipants()
		{
			return new List<Contact>();
		}

		public IActionResult CreateSplit()
		{
			return null;
		}

		public IActionResult EditSplit()
		{
			//can edit split whole or just the interacted status
			return null;
		}
		public IActionResult DeleteSplit()
		{
			return null;
		}


		public IActionResult PayDues()
		{
			return null;
		}
	}
}
