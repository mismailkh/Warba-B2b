using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class UpdateLookupStatusRequestDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }

    }
}
