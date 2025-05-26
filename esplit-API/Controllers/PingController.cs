using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Common;

namespace esplit_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PingController : ControllerBase
	{
		public PingController() { }
		[HttpGet]
		public IActionResult PingDb()
		{
			try
			{
				if (dbMehods.PingDatabase())
					return Ok("Database is reachable.");
				else
					return StatusCode(500, "Unexpected DB response.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Database error: {ex.Message}");
			}
		}
	}
}
