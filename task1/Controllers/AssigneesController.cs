using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task1.Data;
using task1.Data.DTOs;
using task1.Data.models;

namespace task1.Controllers
{
    public class AssigneesController : Controller
    {
        public AssigneesController(AppDbContaxt db)
        {
            _db = db;
        }
        private readonly AppDbContaxt _db;
        [HttpPost]
        public async Task<IActionResult> ADDAssignee(Assignee assignee)
        {
            var emailExists = await _db.Assignees.AnyAsync(a => a.Email == assignee.Email);
            if (emailExists)
            {
                return Conflict(new { message = "Email already exists." });

            }
            assignee.Id = Guid.NewGuid();
            _db.Assignees.Add(assignee);
            await _db.SaveChangesAsync();
            return Ok(assignee);
        }


        [HttpGet("/api/assignees/{id}/tasks")]
        public async Task<IActionResult> GetAssigneeTasks(Guid id, string? status, string? priority, DateTime? dueBefore, string? search, int page = 1, int pageSize = 10)
        {

            var query = _db.Tasks.Where(t => t.AssigneeId == id).AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(t => t.Status.ToString() == status);
            if (!string.IsNullOrWhiteSpace(priority))
                query = query.Where(t => t.Priority.ToString() == priority);
            if (dueBefore.HasValue)
                query = query.Where(t => t.DueDate <= dueBefore);
            if (!string.IsNullOrWhiteSpace(search)) query = query.Where(t => t.Title.Contains(search) || t.Description.Contains(search));

            var totalCount = await query.CountAsync();
            var tasks = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = tasks
            });
        }
    }
}

        

