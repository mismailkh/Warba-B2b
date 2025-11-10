using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.Product.ResponseDtos
{
    public class ProcessListResponseDto
    {
        public int ProcessTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscount { get; set; } = false;
    }
}
