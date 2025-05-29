using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;

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

		public List<Contact> GetContactsByUserID()
		{
			return new List<Contact>();
		}

		public IActionResult DeleteContact()
		{
			return null;
		}

		public IActionResult InteractConnectionRequest()
		{
			return null;
		}

		public IActionResult GetConnectionRequests()
		{
			return null;
		}
	}
}
