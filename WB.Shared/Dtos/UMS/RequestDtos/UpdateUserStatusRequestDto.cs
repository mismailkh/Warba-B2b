using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class UpdateUserStatusRequestDto
    {
        public string UserId { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }
    }
}
