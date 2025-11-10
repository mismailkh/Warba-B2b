using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.CarMake.Request
{
    public class UpdateCarMakeRequestDto
    {
        public Guid Id { get; set; }
        public string CarMakeEn { get; set; }
        public string CarMakeAr { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
