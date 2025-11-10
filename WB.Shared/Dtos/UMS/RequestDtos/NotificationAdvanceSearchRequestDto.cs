using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class NotificationAdvanceSearchRequestDto
    {
        public string? SenderName { get; set; }

        public string? NotificationMessage { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Culture {  get; set; }
        public string UserId { get; set; }

        public bool ViewAll { get; set; }  
        public int TabValue{ get; set; }
    }
}
