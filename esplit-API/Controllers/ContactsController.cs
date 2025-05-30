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
		public IActionResult GetContacts(int userID, string connectionStatus = "APPROVED")
		{
			List<ContactDto> connections = contactService.GetContacts(userID, connectionStatus);
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
		public IActionResult InteractContactRequest(int contactID, string connectionStatus)
		{
			if(contactService.InteractContact(contactID, connectionStatus))
			{
				return Ok($"Connection {connectionStatus} successful.");
			}
			else
			{
				return BadRequest($"Failed to {connectionStatus} with connection.");
			}

		}

		[HttpGet]
		[Route("GetConnectionRequests")]
		public IActionResult GetContactRequests(int userID, string actionType,string connectionStatus = "PENDING")
		{
			List<ContactDto> connections = contactService.GetContacts(userID, connectionStatus, actionType);
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
