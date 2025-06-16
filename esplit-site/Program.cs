using Biz.Services;
using Common.Utils;
using DataAccess;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace esplit_site
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
			var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

			builder.Services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters {
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtOptions.Issuer,
						ValidAudience = jwtOptions.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
						NameClaimType = "name"
					};
				});

			builder.Services.AddAuthorization();
			builder.Services.AddMemoryCache();
			builder.Services.AddHttpContextAccessor();

			builder.Services.AddScoped<CacheService>();
			builder.Services.AddScoped<Identity>();
			builder.Services.AddScoped<NotificationService>();

			builder.Services.AddControllersWithViews();
			DBMethods._connectionString = builder.Configuration.GetConnectionString(Environment.MachineName);

			var app = builder.Build();

			
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseMiddleware<AuthMiddleware>();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=u}/{action=Index}");

			app.Run();
		}
	}
}
