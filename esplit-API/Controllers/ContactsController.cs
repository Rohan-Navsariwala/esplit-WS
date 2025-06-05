using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;
using System.Data;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		ContactService contactService;
		public ContactsController(NotificationService _notifySerive, CacheService _cache, Identity _common) 
		{
			contactService = new ContactService(_cache, _notifySerive, _common);
		}

		[Authorize]
		[HttpGet]
		public IActionResult GetContacts(string contactStatus = "APPROVED")
		{
			ContactStatus status = (ContactStatus)Enum.Parse(typeof(ContactStatus), contactStatus);
			List<ContactDto> connections = contactService.GetContacts(status);
			if(connections != null && connections.Count > 0)
			{
				return Ok(connections);
			}
			else
			{
				return NotFound("No connections found for the given user.");
			}
		}

		[Authorize]
		[HttpDelete("{contactID}")]
		public IActionResult DeleteContact(int contactID)
		{
			if (contactService.DeleteContact(contactID))
			{
				return Ok("Connection deleted successfully.");
			}
			else
			{
				return BadRequest("Failed to delete connection.");
			}
		}

		[Authorize]
		[HttpPost]
		[Route("CreateConnection")]
		public IActionResult CreateContact([FromBody]string toUserName)
		{
			bool contact = contactService.CreateContact(toUserName);
			if (contact != null)
			{
				//return Ok(new { ContactID = contact });
				return Ok("Contact created successfully");
			}
			else
			{
				return BadRequest("Failed to create connection.");
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("InteractConnectionRequest")]
		public IActionResult InteractContactRequest([FromBody]int contactID, string contactStatus)
		{
			ContactStatus status = (ContactStatus)Enum.Parse(typeof(ContactStatus), contactStatus);

			if (contactService.InteractContact(contactID, status))
			{
				return Ok($"Connection {contactStatus} successful.");
			}
			else
			{
				return BadRequest($"Failed to {contactStatus} with connection.");
			}

		}

		[Authorize]
		[HttpGet]
		[Route("GetConnectionRequests")]
		public IActionResult GetContactRequests(string actionType = "RECEIVED", string contactStatus = "PENDING")
		{
			ContactStatus status = (ContactStatus)Enum.Parse(typeof(ContactStatus), contactStatus);
			ContactRequestDirection dir = (ContactRequestDirection)Enum.Parse(typeof(ContactRequestDirection), actionType);

			List<ContactDto> connections = contactService.GetContacts(status, dir);
			if (connections != null && connections.Count > 0)
			{
				return Ok(connections);
			}
			else
			{
				return NotFound("No connection requests found for the given user.");
			}
		}
	}
}
