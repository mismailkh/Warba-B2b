namespace WB.Shared.Dtos.License.ResponseDtos
{
    public class LicensePlanListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }  
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = default!;
        public Guid LicensePlanVersionId { get; set; }
    }
}
