using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
	public class CommonMethods
	{
		public static (string username, int userid) GetClaims(IEnumerable<Claim> claims)
		{
			string uname = claims.FirstOrDefault(o => o.Type == "name")?.Value.ToString();
			int.TryParse(claims.FirstOrDefault(o => o.Type == "uid")?.Value, out int uid);

			return (uname, uid);
		}
	}
}
