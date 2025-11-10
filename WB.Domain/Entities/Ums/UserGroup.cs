using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;

namespace WB.Domain.Entities.Ums
{
    [Table("USER_GROUP", Schema = "ums")]
    public class UserGroup : EntityBase
    {
        public Guid GroupId { get; set; }
        public string UserId { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
