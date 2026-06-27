using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using task1.Data.Enums;


namespace task1.Data.models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public Enums.Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        [ForeignKey("Assignee")]
        public Guid? AssigneeId { get; set; }
        public Assignee? Assignee { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;






    }
}}
