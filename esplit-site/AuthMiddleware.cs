namespace esplit_site
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;

		public AuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Cookies["JWT_Auth"];

			if (!string.IsNullOrEmpty(token))
			{
				if (!context.Request.Headers.ContainsKey("Authorization"))
				{
					context.Request.Headers.Add("Authorization", token);
				}
			}

			await _next(context);
		}
	}

}
