using Microsoft.EntityFrameworkCore;
using task1.Data.models;

namespace task1.Data
{
    public class AppDbContaxt : DbContext
    {
        public AppDbContaxt(DbContextOptions<AppDbContaxt> options) : base(options)
        {
            
        }
        public DbSet<models.Task> Tasks { get; set; }
        public DbSet<models.Assignee> Assignees { get; set; }
    }
}
