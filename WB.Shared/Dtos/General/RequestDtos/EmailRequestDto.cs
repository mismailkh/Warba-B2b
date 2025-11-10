using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.General.RequestDtos
{
    public class EmailRequestDto
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
