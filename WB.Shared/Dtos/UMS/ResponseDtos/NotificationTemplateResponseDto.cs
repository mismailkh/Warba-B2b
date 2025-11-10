using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class NotificationTemplateResponseDto
    {
        [Key]
        public Guid TemplateId { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }

        public string? BodyEn { get; set; }
        public string? BodyAr { get; set; }
        public string? ChannelName { get; set; }
        public string? EventName { get; set; }
        public int? EventId { get; set; }
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public string? DeletedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
