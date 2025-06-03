using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Biz.Services;
using DataAccess.Repositories;
using System.Diagnostics.Eventing.Reader;
using Common.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly JwtOptions _jwtOptions;
		private readonly CommonMethods _commonMethods;
		public UsersController(IOptions<JwtOptions> jwtOptions, CommonMethods commonMethods) 
		{
			_jwtOptions = jwtOptions.Value;
			_commonMethods = commonMethods;
		}

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

		[Authorize]
		[HttpDelete]
		public IActionResult DeleteAccount()
		{
			UserRepository userRepository = new UserRepository();
			(_, int userID) = _commonMethods.GetClaims(User.Claims);
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
		public IActionResult Register(User user)
		{
			UserService userService = new UserService();
			if (userService.RegisterUser(user))
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
		public IActionResult Login(string userName, string password)
		{
			UserService userService = new UserService();
			User userData = userService.Authenticate(userName, password);
			if(userData != null)
			{
				var claims = new[]
				{
					new Claim("name", userName),
					new Claim("uid", userData.UserID.ToString())
				};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(
					issuer: _jwtOptions.Issuer,
					audience: _jwtOptions.Audience,
					claims: claims,
					expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),
					signingCredentials: creds
				);

				return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), data = userData });
			}
			else
			{
				return Unauthorized();
			}
		}

		[Authorize]
		[HttpGet]
		[Route("Test")]
		public IActionResult Test()
		{
			(string userName, int userID) = _commonMethods.GetClaims(User.Claims);
			return Ok(new { userID, userName });
		}
	}
}
