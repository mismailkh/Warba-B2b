using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Notification
{
    [Table("NOTIFICATION_CHANNEL_LKP", Schema ="notif")]
    public class NotificationChannel
    {
        [Key]
        public int ChannelId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public ICollection<NotificationTemplate> NotificationTemplates { get; set; }

    }
}
