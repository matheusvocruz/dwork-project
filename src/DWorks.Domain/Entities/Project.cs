using System.ComponentModel.DataAnnotations.Schema;

namespace DWorks.Domain.Entities
{
    [Table("Project")]
    public class Project : BaseEntity
    {
        public required string Name { get; set; }
        public long UserId { get; set; }
    }
}
