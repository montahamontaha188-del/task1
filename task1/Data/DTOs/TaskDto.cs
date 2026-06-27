using task1.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace task1.Data.DTOs
{
    public class TaskDto
    {


        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public Enums.Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? Assigneeid { get; set; }
        public string? AssigneeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
