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
		[HttpGet]
		public IActionResult SplitParticipants(int splitid)
		{
			List<ParticipantDto> participants = _splitService.GetParticipants(splitid);
			return PartialView("_ViewParticipants", participants);
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
