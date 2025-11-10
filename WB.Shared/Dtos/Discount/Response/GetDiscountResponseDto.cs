using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.Discount.Response
{
    public class GetDiscountResponseDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int Disc_Percentage { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
