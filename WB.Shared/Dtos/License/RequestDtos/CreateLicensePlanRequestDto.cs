namespace WB.Shared.Dtos.License.RequestDtos
{
    public class CreateLicensePlanRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Sessions { get; set; }     
        public int Duration { get; set; }     
        public double Price { get; set; }
        public bool IsActive { get; set; } = true;
        public int Version { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
