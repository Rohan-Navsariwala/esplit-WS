using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SplitsController : ControllerBase
	{
		SplitsService splitService;
		public SplitsController(NotificationService _notifySerive, CacheService _cache, Identity _common) 
		{ 
			splitService = new SplitsService(_notifySerive, _cache, _common);
		}

		[Authorize]
		[HttpGet]
		[Route("UserCreatedSplits")]
		public IActionResult GetUserCreatedSplits()
		{
			List<SplitInfo> splits = splitService.GetSplits(SplitStatus.OWNED);
			if (splits != null && splits.Count > 0)
			{
				return Ok(splits);
			}
			else
			{
				return BadRequest();
			}
		}

		//public List<Split> GetPendingSplitRequests()
		//{
		//	return new List<Split>();
		//}

		//public List<Split> GetUserDueSplits()
		//{
		//	return new List<Split>();
		//}

		[Authorize]
		[HttpGet]
		[Route("UserInvolvedSplits")]
		public IActionResult GetUserInvolvedSplits()
		{
			List<SplitInfo> splits = splitService.GetSplits(SplitStatus.ALL);
			if (splits != null && splits.Count > 0)
			{
				return Ok(splits);
			}
			else
			{
				return BadRequest();
			}
		}

		[Authorize]
		[HttpPost]
		[Route("SplitParticipants")]
		public IActionResult GetSplitParticipants([FromBody]int splitID)
		{
			List<ParticipantDto> splitContacts = splitService.GetParticipants(splitID);
			if(splitContacts != null  && splitContacts.Count > 0)
			{
				return Ok(splitContacts);
			}
			else
			{
				return BadRequest();
			}

		}

		[Authorize]
		[HttpPost]
		[Route("AddSplitParticipant")]
		public IActionResult AddSplitParticipant([FromBody]SplitContact contact)
		{
			if(splitService.AddSplitParticipant(contact))
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[Authorize]
		[HttpPost]
		public IActionResult CreateSplit([FromBody]Split split)
		{
			if (splitService.CreateSplit(split))
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("EditSplit")]
		public IActionResult EditSplit(int splitID, [FromBody]string splitStatus)
		{
			//can edit split whole or just the interacted status, toggle split request from here
			if(splitService.ToggleSplit(splitID, splitStatus))
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[Authorize]
		[HttpDelete]
		public IActionResult CloseSplit(int splitID)
		{
			//not delete, but set split status to close
			if(splitService.MarkClosed(splitID))
			{
				return Ok("Split Closed");
			}
			else
			{
				return BadRequest("Unable to close the split");
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("PayDue/{splitID}")]
		public IActionResult PayDues(int splitID)
		{
			
			if(splitService.PayDue(splitID))
			{
				return Ok("Marked Payment as Done");
			}
			else
			{
				return BadRequest("Unable to mark done");
			}
		}
	}
}
