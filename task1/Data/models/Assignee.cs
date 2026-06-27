using System.ComponentModel.DataAnnotations;

namespace task1.Data.models
{
    public class Assignee
    {
        [Key] 
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }



    }
}
