using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Notification
{
    [Table("NOTIFICATION_EVENT_PLACEHOLDERS_LKP",Schema ="notif")]
    public class NotificationEventPlaceholders
    {
        [Key]
        public int PlaceHolderId { get; set; }
        public string PlaceHolderName { get; set; }
        public int? EventId { get; set; }
        public NotificationEvent Event { get; set; }
    }
}

