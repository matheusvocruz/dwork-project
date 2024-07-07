using System.ComponentModel.DataAnnotations.Schema;

namespace DWorks.Domain.Entities
{
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        public required string Content { get; set; }
        public long TaskId { get; set; }
    }
}
