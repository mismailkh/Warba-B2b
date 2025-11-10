using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.CarMake.Request
{
    public class AddCarModelRequestDto
    {
        public Guid CarMakeId { get; set; }
        public string CarModelEn { get; set; }
        public string CarModelAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
