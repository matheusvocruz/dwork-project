using DWorks.Domain.Enums.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWorks.Domain.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public UserTypeEnum Type { get; set; }
    }
}
