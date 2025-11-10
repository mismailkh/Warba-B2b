using WB.Shared.Dtos.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class UpdateNotificationTemplateRequestDto
    {

        public Guid TemplateId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public string BodyEn { get; set; }
        public string BodyAr { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
