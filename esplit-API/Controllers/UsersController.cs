using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		public UsersController() { }

		public User GetUser()
		{
			//here you have to define logic to get by either id or name
			return new User();
		}

		public IActionResult DeleteUser()
		{
			//basically here i will be setting status to inactive
			return null;
		} 

		public IActionResult CreateUser()
		{
			return null;
		}
	}
}
