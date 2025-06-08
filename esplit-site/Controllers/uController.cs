using System.Diagnostics;
using Biz.Services;
using esplit_site.Models;
using Microsoft.AspNetCore.Mvc;
using Common.Types;
using Common.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace esplit_site.Controllers
{
	[Route("{controller}")]
	public class uController : Controller
	{
		private readonly ILogger<uController> _logger;
		private readonly UserService _userService;
		private readonly JwtOptions _jwtOptions;


		public uController(ILogger<uController> logger, IOptions<JwtOptions> jwtOptions)
		{
			_logger = logger;
			_userService = new UserService();
			_jwtOptions = jwtOptions.Value;

			//ViewBag.RequestPath = HttpContext.Request.Path;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return RedirectToAction("Login");
		}

		[HttpGet]
		[Route("Login")]
		public IActionResult Login()
		{
			ViewBag.RequestPath = HttpContext.Request.Path;
			return View("Index");
		}

		[HttpGet]
		[Route("Signup")]
		public IActionResult Signup()
		{
			ViewBag.RequestPath = HttpContext.Request.Path;
			return View("Index");
		}

		[HttpPost]
		[Route("LoginPost")]
		public IActionResult LoginPost(string UserName, string PasswordHash)
		{
			UserService userService = new UserService();
			User userData = userService.Authenticate(UserName, PasswordHash);
			if (userData != null)
			{
				var claims = new[]
				{
					new Claim("name", UserName),
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
				
				var bearertoken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);

				Response.Cookies.Append("JWT_Auth", bearertoken, new CookieOptions {
					HttpOnly = true,
					Secure = false,
					SameSite = SameSiteMode.Lax,
					Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes)
				});

				return RedirectToAction("Index", "Dashboard");
			}
			else
			{
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[Route("SignupPost")]
		public IActionResult SignupPost(string UserName, string FullName, string PasswordHash)
		{
			bool registrationStatus = _userService.RegisterUser(new User() 
			{ 
				UserName = UserName,
				FullName = FullName,
				PasswordHash = PasswordHash
			});

			if(registrationStatus)
			{
				return RedirectToAction("Login");
			}
			else
			{
				ModelState.AddModelError("", "Invalid credentials"); //find out what it does
				return View("Index");
			}
		}

	}
}
