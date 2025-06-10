using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Biz.Services;
using Common.Utils;
using Common.Types;

namespace esplit_site.Controllers
{
	public class DashboardController : Controller
	{
		private SplitsService _splitService;
		public DashboardController(NotificationService notificationService, Identity _claims, CacheService _cache)
		{
			_splitService = new SplitsService(notificationService, _cache, _claims);
		}

		[Authorize]
		public IActionResult Index()
		{
			List<List<SplitInfo>> splits = new List<List<SplitInfo>>();
			splits.Add(_splitService.GetSplits(SplitStatus.OWNED));
			splits.Add(_splitService.GetSplits(SplitStatus.ALL));

			return View(splits);
		}

		//return all the splits for the given type
		public IActionResult Splits()
		{
			return View();
		}

		//this is gonna return the dialogue box of participants of specific split
		[Authorize]
		[HttpGet]
		public IActionResult SplitParticipants(int splitid)
		{
			List<ParticipantDto> participants = _splitService.GetParticipants(splitid);
			return PartialView("_ViewParticipants", participants);
		}

		[Authorize]
		[HttpPost]
		[Route("AddParticipant")]
		public IActionResult AddParticipant(SplitContact participant)
		{
			if (_splitService.AddSplitParticipant(participant))
			{
				return Ok(new { success = true, message = "Participant added successfully." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to add participant." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("ApproveSplit")]
		public IActionResult Approve(int SplitID)
		{
			if(_splitService.ToggleSplit(SplitID, "APPROVED_UNPAID"))
			{
				return Ok(new { success = true, message = "Split approved successfully." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to approve split." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("RejectSplit")]
		public IActionResult Reject(int SplitID)
		{
			if (_splitService.ToggleSplit(SplitID, "REJECTED"))
			{
				return Ok(new { success = true, message = "Successfully rejected split request." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to reject split request." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("PayDue")]
		public IActionResult Pay(int SplitID)
		{
			if(_splitService.PayDue(SplitID))
			{
				return Ok(new { success = true, message = "Marked as Done" });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to mark done" });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("CloseSplit")]
		public IActionResult Close(int SplitID)
		{
			if(_splitService.MarkClosed(SplitID))
			{
				return Ok(new { success = true, message = "Split closed successfully." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to close split." });
			}
		}
	}
}
