using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.Product.RequestDtos
{
    public class UpdateProductRequestDto : EntityBaseDto
    {
        public int ProductId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
