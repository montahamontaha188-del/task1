using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task1.Data;
using task1.Data.DTOs;
using task1.Data.models;

namespace task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public TasksController(AppDbContaxt db)
        {
            _db = db;
        }
        private readonly AppDbContaxt _db;
        [HttpPost]
        public async Task<IActionResult> CreateTask(Data.models.Task task)
        {

            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            //return Ok(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);

        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            // var task = await _db.Tasks.FindAsync(id);
            var test = await _db.Tasks.Include(t => t.Assignee)
                 .FirstOrDefaultAsync(t => t.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteTasks(List<Guid> ids)
        {
            var tasks = await _db.Tasks.Where(t => ids.Contains(t.Id)).ToListAsync();
            if (tasks == null )
            {
                return NotFound();
            }
             _db.Tasks.RemoveRange(tasks);
            await _db.SaveChangesAsync();
            return Ok(new {deleted_number = tasks.Count });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, TaskUpdateDto updateDto)

        {
            var test = await _db.Tasks .Include(t => t.Assignee) .FirstOrDefaultAsync(t => t.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            test.Title = updateDto.Title;
            test.Description = updateDto.Description;
            test.Status = updateDto.Status;
            test.Priority = updateDto.Priority;
            test.DueDate = updateDto.DueDate;
            test.AssigneeId = updateDto.Assigneeid;
            test.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return Ok(test);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateTaskStatus(Guid id, StatusUpdateDto updateDto)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.Status = updateDto.Status;
            task.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return Ok(task);
        }

    }
}


