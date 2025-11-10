using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveTodoItemRequestDto
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
