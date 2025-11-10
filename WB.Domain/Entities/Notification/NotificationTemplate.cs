using WB.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Notification
{
    [Table("NOTIFICATION_TEMPLATE", Schema ="notif")]
    public class NotificationTemplate : EntityBase
    {
        [Key]
        public Guid TemplateId { get; set; }
        public int EventId { get; set; }
        public int ChannelId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? SubjectEn { get; set; }
        public string? SubjectAr { get; set; }
        public string BodyEn { get; set; }
        public string BodyAr { get; set; }
        public string? Footer { get; set; }
        public bool IsActive { get; set; }
        public NotificationEvent? Event { get; set; }
        public NotificationChannel? Channel { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
