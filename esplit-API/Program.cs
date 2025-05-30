using DataAccess;
using Microsoft.Extensions.Configuration;

namespace esplit_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            dbMethods._connectionString = builder.Configuration.GetConnectionString((Environment.MachineName == "ALBATROSS") ? "pString" : "wString");

			var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

			//builder.Services.AddCors(options =>
			//{
			//	options.AddDefaultPolicy(policy =>
			//	{
			//		policy.AllowAnyOrigin()
			//			  .AllowAnyMethod()
			//			  .AllowAnyHeader();
			//	});
			//});

			//app.UseCors(); // Add this before app.UseAuthorization()

			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
