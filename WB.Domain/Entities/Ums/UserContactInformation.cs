using WB.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Ums
{
    [Table("USER_CONTACT_INFORMATION", Schema = "ums")]
    public class UserContactInformation : EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactNumber { get; set; }
        public bool IsPrimary { get; set; }
        public User User { get; set; }
    }
}
