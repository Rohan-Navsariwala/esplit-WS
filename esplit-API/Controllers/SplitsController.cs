using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SplitsController : ControllerBase
	{
		SplitsService splitService;
		public SplitsController() 
		{ 
			splitService = new SplitsService();
		}

		[HttpGet]
		[Route("UserCreated")]
		public IActionResult GetUserCreatedSplits(int userID)
		{
			List<SplitInfo> splits = splitService.GetSplits(userID, SplitStatus.OWNED);
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

		[HttpGet]
		[Route("UserNonCreated")]
		public IActionResult GetNonUserCreatedSplits(int userID)
		{
			List<SplitInfo> splits = splitService.GetSplits(userID, SplitStatus.ALL);
			if (splits != null && splits.Count > 0)
			{
				return Ok(splits);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetSplitParticipants")]
		public IActionResult GetSplitParticipants(int splitID)
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

		[HttpGet]
		[Route("AddSplitParticipant")]
		public IActionResult AddSplitParticipant(SplitContact contact)
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

		[HttpPost]
		public IActionResult CreateSplit(Split split)
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

		[HttpPost]
		[Route("EditSplit")]
		public IActionResult EditSplit(int splitID, int userID,[FromBody] string splitStatus)
		{
			//can edit split whole or just the interacted status, toggle split request from here
			if(splitService.ToggleSplit(userID, splitID, splitStatus))
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete]
		public IActionResult CloseSplit(int userID, int splitID)
		{
			//not delete, but set split status to close
			if(splitService.MarkClosed(userID, splitID))
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("PayDue")]
		public IActionResult PayDues(int splitID, int userID)
		{
			
			if(splitService.PayDue(splitID, userID))
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
