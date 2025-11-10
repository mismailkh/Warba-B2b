using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.CarMake.Response
{
    public class GetCarMakeResponseDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public string CarMakeEn { get; set; }
        public string CarMakeAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
