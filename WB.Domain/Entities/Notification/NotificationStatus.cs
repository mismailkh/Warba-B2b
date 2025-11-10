using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Notification
{
    [Table("NOTIFICATION_STATUS_LKP", Schema ="notif")]
    public class NotificationStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
