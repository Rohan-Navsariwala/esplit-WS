using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;
using System.Data;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		ContactService contactService;
		public ContactsController() 
		{
			contactService = new ContactService();
		}

		[HttpGet]
		public IActionResult GetContacts(int userID, string contactStatus = "APPROVED")
		{
			ContactStatus status = (ContactStatus)Enum.Parse(typeof(ContactStatus), contactStatus);
			List<ContactDto> connections = contactService.GetContacts(userID, status);
			if(connections != null && connections.Count > 0)
			{
				return Ok(connections);
			}
			else
			{
				return NotFound("No connections found for the given user.");
			}
		}

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

		[HttpPost]
		[Route("CreateConnection")]
		public IActionResult CreateContact(int userID, string toUserName)
		{
			int contactID = contactService.CreateContact(userID, toUserName);
			if (contactID > 0)
			{
				return Ok(new { ContactID = contactID });
			}
			else
			{
				return BadRequest("Failed to create connection.");
			}
		}

		[HttpPost]
		[Route("InteractConnectionRequest")]
		public IActionResult InteractContactRequest(int contactID, string contactStatus)
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

		[HttpGet]
		[Route("GetConnectionRequests")]
		public IActionResult GetContactRequests(int userID, string actionType, string contactStatus = "PENDING")
		{
			ContactStatus status = (ContactStatus)Enum.Parse(typeof(ContactStatus), contactStatus);
			List<ContactDto> connections = contactService.GetContacts(userID, status, actionType);
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
