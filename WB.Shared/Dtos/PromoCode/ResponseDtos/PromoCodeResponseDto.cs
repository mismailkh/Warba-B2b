using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;
using WB.Shared.Enums;

namespace WB.Shared.Dtos.PromoCode.ResponseDtos
{
    public class PromoCodeResponseDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public int DiscountType { get; set; }
        public int DiscountValue { get; set; }
        public int UsageLimit { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int AssignmentType { get; set; }
        public bool UnlimitedUsage { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public bool IsGlobal { get; set; }

        [NotMapped]
        public string DiscountTypeName =>
    ((GeneralEnums.DiscountTypeEnum)DiscountType).ToString();
        [NotMapped]
        public IEnumerable<Guid> SelectedSiteIds { get; set; } = new List<Guid>();
    }
}
