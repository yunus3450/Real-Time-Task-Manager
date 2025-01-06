using Microsoft.EntityFrameworkCore;
using RealtimeTaskManagerAPI.Data;

namespace RealtimeTaskManagerAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();

			// Add Swagger generation service
			builder.Services.AddSwaggerGen();

			// Add DbContext
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Add CORS policy
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin() // Her kaynaktan gelen isteklere izin verir
						  .AllowAnyMethod()  // Her HTTP metoduna izin verir
						  .AllowAnyHeader(); // Her header'a izin verir
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
				});
			}

			
			app.UseCors("AllowAll");

			app.UseHttpsRedirection();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
