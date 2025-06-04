using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Common.Utils
{
	public class Identity
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public Identity(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public (string username, int userid) GetClaims()
		{
			IEnumerable<Claim> claims = _httpContextAccessor.HttpContext?.User?.Claims;

			if (claims != null)
			{
				string uname = claims.FirstOrDefault(o => o.Type == "name")?.Value.ToString();
				int.TryParse(claims.FirstOrDefault(o => o.Type == "uid")?.Value, out int uid);
				return (uname, uid);
			}
			else
			{
				return (string.Empty, 0);
			}
		}
	}
}
