using WB.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Notification
{
    [Table("NOTIFICATION_EVENT", Schema ="notif")]
    public class NotificationEvent : EntityBase
    {
        [Key]
        public int EventId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public int ReceiverTypeId { get; set; }
        public Guid? ReceiverTypeRefId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<NotificationEventPlaceholders>? NotificationEventPlaceholders { get; set; }
        public ICollection<NotificationTemplate>? NotificationTemplates { get; set; }
    }
}

