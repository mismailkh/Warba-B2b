using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class SaveUserResponseDto
    {
        public bool Succeeded { get ; set; }
        public dynamic? User { get; set; }
        public string? ErrorCode { get; set; }
    }
}
