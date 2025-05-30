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
		ConnectionService connectionService;
		public ContactsController() 
		{
			connectionService = new ConnectionService();
		}

		[HttpGet]
		public IActionResult GetContacts(int userID, string connectionStatus = "APPROVED")
		{
			List<ConnectionDto> connections = connectionService.GetConnections(userID, connectionStatus);
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
			if (connectionService.DeleteConnection(contactID))
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
		public IActionResult CreateConnection(int userID, string toUserName)
		{
			int contactID = connectionService.CreateConnection(userID, toUserName);
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
		public IActionResult InteractConnectionRequest(int contactID, string connectionStatus)
		{
			if(connectionService.InteractConnection(contactID, connectionStatus))
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
		public IActionResult GetConnectionRequests(int userID, string connectionStatus = "PENDING")
		{
			List<ConnectionDto> connections = connectionService.GetConnections(userID, connectionStatus);
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
