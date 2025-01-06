using Microsoft.EntityFrameworkCore;
using RealtimeTaskManagerAPI.Models;

namespace RealtimeTaskManagerAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<TaskModel> Tasks { get; set; }
	}
}
