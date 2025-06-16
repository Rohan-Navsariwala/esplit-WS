using Biz.Services;
using Common.Types;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace esplit_site.Controllers
{
	[Route("{controller}")]
	public class DashboardController : BaseController
	{
		private SplitsService _splitService;
		private ContactService _contactService;
		public DashboardController(NotificationService notificationService, Identity _claims, CacheService _cache) : base(_claims)
		{
			_splitService = new SplitsService(notificationService, _cache, _claims);
			_contactService = new ContactService(_cache, notificationService, _claims);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Index()
		{
			DashboardViewModel dashboardViewModel = new DashboardViewModel();

			List<List<SplitInfo>> splits = new List<List<SplitInfo>>();
			splits.Add(_splitService.GetSplits(SplitStatus.OWNED));
			List<SplitInfo> allSplits = _splitService.GetSplits(SplitStatus.ALL);
			splits.Add(allSplits.Where(s => s.SplitParticipantStatus == SplitStatus.PENDING_APPROVAL).ToList());
			splits.Add(allSplits.Where(s => s.SplitParticipantStatus == SplitStatus.APPROVED_UNPAID).ToList());
			dashboardViewModel.SplitsData = splits;

			List<ContactDto> contacts = new List<ContactDto>();
			contacts = _contactService.GetContacts(ContactStatus.APPROVED);
			dashboardViewModel.Contacts = contacts;

			return View(dashboardViewModel);
		}

		//return all the splits for the given type
		[HttpGet]
		[Route("Splits")]
		public IActionResult Splits()
		{
			return View();
		}

		//this is gonna return the dialogue box of participants of specific split
		[Authorize]
		[HttpGet]
		[Route("SplitParticipants")]
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

		[HttpGet]
		[Route("ModelTest")]
		public IActionResult ModelTest()
		{
			return View("_AddSplitModal");
		}
	}
}
