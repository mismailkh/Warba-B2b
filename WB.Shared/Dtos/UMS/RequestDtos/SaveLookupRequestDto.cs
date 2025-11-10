using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveLookupRequestDto : EntityBaseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? CityId { get; set; }
        public decimal? MHPSeatCost { get; set; }
        public decimal? OperationalSeatCost { get; set; }
        public decimal? AdministrationalSeatCost { get; set; }
        public decimal? SessionCost { get; set; }
        public int? CategoryId { get; set; }
    }
}
