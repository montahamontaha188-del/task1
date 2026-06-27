namespace task1.Data.DTOs
{
    public class TaskUpdateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public Enums.Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? Assigneeid { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
