using Microsoft.AspNetCore.Mvc;
using RealtimeTaskManagerAPI.Data;
using RealtimeTaskManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RealtimeTaskManagerAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TasksController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TasksController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTasks()
		{
			var tasks = await _context.Tasks.ToListAsync();
			return Ok(tasks);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTask(TaskModel task)
		{
			task.CreatedAt = DateTime.Now;
			_context.Tasks.Add(task);
			await _context.SaveChangesAsync();
			return Ok(task);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTask(int id, TaskModel task)
		{
			var existingTask = await _context.Tasks.FindAsync(id);
			if (existingTask == null) return NotFound();

			existingTask.Title = task.Title;
			existingTask.Description = task.Description;
			existingTask.IsCompleted = task.IsCompleted;
			await _context.SaveChangesAsync();

			return Ok(existingTask);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTask(int id)
		{
			var task = await _context.Tasks.FindAsync(id);
			if (task == null) return NotFound();

			_context.Tasks.Remove(task);
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
