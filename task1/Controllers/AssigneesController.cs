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
        [HttpGet("{id:guid} / tasks")]

    }
}

        

