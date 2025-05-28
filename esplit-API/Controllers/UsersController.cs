using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;
using DataAccess.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		public UsersController() { }

		[HttpGet("{user}")]
		public IActionResult GetUser(string user)
		{
			UserRepository userRepository = new UserRepository();
			User userData;
			
			if(int.TryParse(user, out int userID))
			{
				userData = userRepository.GetUserById(userID);
			}
			else
			{
				userData = userRepository.GetUserByUserName(user);
			}

			if (userData != null)
			{
				return Ok(userData);
			}
			else
			{
				return NotFound("User Not Found :(");
			}
		}

		[HttpDelete("{userID}")]
		public IActionResult DeleteUser(string userID)
		{
			UserRepository userRepository = new UserRepository();
			if (userRepository.DeleteUser(userID))
			{
				return Ok("User Deleted Successfully");
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		public IActionResult CreateUser(User user)
		{
			RegistrationService registrationService = new RegistrationService();
			if (registrationService.RegisterUser(user))
			{
				return Ok("User Created Successfully :)");
			}
			else
			{
				return Conflict("User Already Exists :(");
			}
		}

		[HttpPost]
		[Route("auth")]
		public IActionResult AuthenticateUser(string userName, string password)
		{
			AuthService authService = new AuthService();
			User userData = authService.Authenticate(userName, password);
			if(userData != null)
			{
				return Ok(userData);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
