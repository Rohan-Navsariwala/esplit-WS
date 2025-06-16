using Microsoft.AspNetCore.Mvc;
using Common.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace esplit_site.Controllers
{
	public class BaseController : Controller
	{
		protected readonly string _UserName;
		protected readonly int _UserID;
		public BaseController(Identity claims)
		{
			(_UserName, _UserID) = claims.GetClaims();
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
			//this is done to be used in views only
			ViewBag.UserID = _UserID;
			ViewBag.UserName = _UserName;
		}
	}
}
