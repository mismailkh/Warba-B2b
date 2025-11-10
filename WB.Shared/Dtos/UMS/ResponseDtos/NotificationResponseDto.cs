using WB.Shared.Dtos.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public record NotificationResponseDto
    {
        [Key]
        public Guid NotificationId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string NotificationURL { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EventId { get; set; }
    }
}
