namespace WB.Shared.Dtos.Organization.RequestDtos
{
    public class UpdateOrganizationTypeRequestDto
    {
        public int TypeId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
