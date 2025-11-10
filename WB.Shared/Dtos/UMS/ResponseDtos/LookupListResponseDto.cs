using WB.Shared.Dtos.General;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class LookupListResponseDto : EntityBaseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public int? RegionId { get; set; }
        [NotMapped]
        public int? StateId { get; set; }
        [NotMapped]
        public int? CountryId { get; set; }
        [NotMapped]
        public int? CityId { get; set; }
        [NotMapped]
        public int? CategoryId { get; set; }
        public decimal? MHPSeatCost { get; set; }
        public decimal? OperationalSeatCost { get; set; }
        public decimal? AdministrationalSeatCost { get; set; }
        public decimal? SessionCost { get; set; }
    }
}
