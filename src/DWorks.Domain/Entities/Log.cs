using System.ComponentModel.DataAnnotations.Schema;

namespace DWorks.Domain.Entities
{
    [Table("Log")]
    public class Log : BaseEntity
    {
        public required string Entity { get; set; }
        public long EntityId { get; set; }
        public required string Operation { get; set; }
        public string? Field { get; set; }
        public string? Old { get; set; }
        public required string New { get; set; }
        public string? Parent { get; set; }
        public long? ParentId { get; set; }
    }
}
