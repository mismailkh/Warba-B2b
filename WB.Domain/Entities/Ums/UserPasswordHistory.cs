using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Ums
{
    [Table("USER_PASSWORD_HISTORY", Schema = "ums")]
    public class UserPasswordHistory
    {
        [Key]
        public Guid HistoryId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
    }
}
