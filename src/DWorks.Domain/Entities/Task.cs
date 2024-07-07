using DWorks.Domain.Enums.Task;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWorks.Domain.Entities
{
    [Table("Task")]
    public class Task : BaseEntity
    {
        public required string Tittle { get; set; }
        public required string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public TaskPriorityEnum Priority { get; set; }
        public long ProjectId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
